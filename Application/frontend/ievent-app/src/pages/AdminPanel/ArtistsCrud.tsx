import React, { useState, useEffect } from "react";
import { getAllArtists, addArtist, updateArtist, deleteArtist } from "../../api/artists";
import { GetAllArtistDto, AddArtistDto, UpdateArtistDto } from "../../types/dtos/artistsDto";
import ArtistPopup from "./ArtistsPopup";


const ArtistsCrud: React.FC = () => {
  const [artists, setArtists] = useState<GetAllArtistDto[]>([]);
  const [popupMode, setPopupMode] = useState<"add" | "edit" | null>(null);
  const [selectedArtist, setSelectedArtist] = useState<GetAllArtistDto | null>(
    null
  );

  useEffect(() => {
    const fetchArtists = async () => {
      try {
        const data = await getAllArtists();
        setArtists(data);
      } catch (error) {
        console.error("Error fetching artists:", error);
      }
    };
    fetchArtists();
  }, []);

  const handleAdd = async (artist: AddArtistDto) => {
    try {
      await addArtist(artist);
      setArtists(await getAllArtists());
    } catch (error) {
      console.error("Error adding artist:", error);
    }
  };

  const handleUpdate = async (artist: UpdateArtistDto) => {
    try {
      await updateArtist(artist);
      setArtists(await getAllArtists());
    } catch (error) {
      console.error("Error updating artist:", error);
    }
  };

  const handleDelete = async (id: number) => {
    try {
      await deleteArtist(id);
      setArtists(await getAllArtists());
    } catch (error) {
      console.error("Error deleting artist:", error);
    }
  };

  const handleSave = (name: string, description: string) => {
    if (popupMode === "add") {
      handleAdd({ name, description });
    } else if (popupMode === "edit" && selectedArtist) {
      handleUpdate({ id: selectedArtist.id, name, description });
    }
    setPopupMode(null);
    setSelectedArtist(null);
  };

  return (
    <div className="crud-container">
      <h2>Artists</h2>
      <button onClick={() => setPopupMode("add")}>Add Artist</button>
      <ul>
        {artists.map((artist) => (
          <li key={artist.id}>
            <div>
              <strong>{artist.name}</strong>
              <p>{artist.description}</p>
            </div>
            <button
              onClick={() => {
                setSelectedArtist(artist);
                setPopupMode("edit");
              }}
            >
              Edit
            </button>
            <button onClick={() => handleDelete(artist.id)}>Delete</button>
          </li>
        ))}
      </ul>
      {popupMode && (
        <ArtistPopup
          mode={popupMode}
          initialData={
            popupMode === "edit" && selectedArtist
              ? { name: selectedArtist.name, description: selectedArtist.description }
              : { name: "", description: "" }
          }
          onSave={(data:any) => handleSave(data.name, data.description)}
          onClose={() => setPopupMode(null)}
        />
      )}
    </div>
  );
};

export default ArtistsCrud;
