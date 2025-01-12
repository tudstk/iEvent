import React, { useState, useEffect } from "react";
import GenrePopup from "./GenrePopup";
import { getAllGenres, deleteGenre, addGenre, updateGenre } from "../../api/genres";
import { GetAllGenresDto, AddGenreDto, UpdateGenreDto } from "../../types/dtos/genreDto";


const GenresCrud: React.FC = () => {
  const [genres, setGenres] = useState<GetAllGenresDto[]>([]);
  const [popupMode, setPopupMode] = useState<"add" | "edit" | null>(null);
  const [selectedGenre, setSelectedGenre] = useState<GetAllGenresDto | null>(
    null
  );

  useEffect(() => {
    const fetchGenres = async () => {
      try {
        const data = await getAllGenres();
        setGenres(data);
      } catch (error) {
        console.error("Error fetching genres:", error);
      }
    };

    fetchGenres();
  }, []);

  const handleDelete = async (id: number) => {
    try {
      await deleteGenre(id);
      setGenres(genres.filter((genre) => genre.id !== id));
    } catch (error) {
      console.error("Error deleting genre:", error);
    }
  };

  const handleSave = async (data: { name: string; description: string }) => {
    try {
      if (popupMode === "add") {
        const newGenre: AddGenreDto = { name: data.name, description: data.description };
        await addGenre(newGenre);
        setGenres([...genres, { id: genres.length + 1, ...data }]); // Replace with updated fetch if needed
      } else if (popupMode === "edit" && selectedGenre) {
        const updatedGenre: UpdateGenreDto = {
          id: selectedGenre.id,
          name: data.name,
          description: data.description,
        };
        await updateGenre(updatedGenre);
        setGenres(
          genres.map((genre) =>
            genre.id === selectedGenre.id ? { ...genre, ...data } : genre
          )
        );
      }
      setPopupMode(null);
      setSelectedGenre(null);
    } catch (error) {
      console.error("Error saving genre:", error);
    }
  };

  return (
    <div className="crud-container">
      <h2>Genres</h2>
      <button onClick={() => setPopupMode("add")}>Add Genre</button>
      <ul>
        {genres.map((genre) => (
          <li key={genre.id}>
            <strong>{genre.name}</strong> - {genre.description}
            <button
              onClick={() => {
                setSelectedGenre(genre);
                setPopupMode("edit");
              }}
            >
              Edit
            </button>
            <button onClick={() => handleDelete(genre.id)}>Delete</button>
          </li>
        ))}
      </ul>
      {popupMode && (
        <GenrePopup
          mode={popupMode}
          initialData={
            popupMode === "edit" && selectedGenre
              ? { name: selectedGenre.name, description: selectedGenre.description }
              : { name: "", description: "" }
          }
          onSave={handleSave}
          onClose={() => {
            setPopupMode(null);
            setSelectedGenre(null);
          }}
        />
      )}
    </div>
  );
};

export default GenresCrud;
