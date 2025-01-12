import React, { useEffect, useState } from "react";

import Select from "react-select";
import "./Profile.css";
import { getAllArtists } from "../../api/artists";
import { getAllGenres } from "../../api/genres";
import { getAllLocations } from "../../api/location";
import { getProfile, updateProfile } from "../../api/profileReq";
import { DropdownOption, ModifyProfileDto } from "../../types/dtos/profile";

const Profile: React.FC = () => {
  const [userName, setUserName] = useState("");
  const [email, setEmail] = useState("");
  const [artists, setArtists] = useState<DropdownOption[]>([]);
  const [genres, setGenres] = useState<DropdownOption[]>([]);
  const [locations, setLocations] = useState<DropdownOption[]>([]);
  const [artistIds, setArtistIds] = useState<number[]>([]);
  const [genreIds, setGenreIds] = useState<number[]>([]);
  const [locationIds, setLocationIds] = useState<number[]>([]);
  const [allArtists, setAllArtists] = useState<DropdownOption[]>([]);
  const [allGenres, setAllGenres] = useState<DropdownOption[]>([]);
  const [allLocations, setAllLocations] = useState<DropdownOption[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const profile = await getProfile();
        setUserName(profile.userName);
        setEmail(profile.email);
        setArtists(profile.myArtists);
        setGenres(profile.myGenres);
        setLocations(profile.myLocations);
        setArtistIds(profile.myArtists.map((artist) => artist.id));
        setGenreIds(profile.myGenres.map((genre) => genre.id));
        setLocationIds(profile.myLocations.map((location) => location.id));

        const [artistsData, genresData, locationsData] = await Promise.all([
          getAllArtists(),
          getAllGenres(),
          getAllLocations(),
        ]);

        setAllArtists(artistsData);
        setAllGenres(genresData);
        setAllLocations(locationsData);
      } catch (error) {
        console.error("Error fetching profile data:", error);
      }
    };

    fetchData();
  }, []);

  const handleSave = async () => {
    try {
      const profile: ModifyProfileDto = {
        userName,
        userEmail: email,
        artistsIds: artistIds,
        genresIds: genreIds,
        locationsIds: locationIds,
        eventTypesIds: [],
      };
      await updateProfile(profile);
      alert("Profile updated successfully!");
    } catch (error) {
      console.error("Error saving profile:", error);
      alert("Failed to save profile.");
    }
  };

  const handleAddOption = (
    selectedOption: DropdownOption | null,
    setFunction: React.Dispatch<React.SetStateAction<DropdownOption[]>>,
    setIdFunction: React.Dispatch<React.SetStateAction<number[]>>,
    currentList: DropdownOption[]
  ) => {
    if (selectedOption && !currentList.some((item) => item.id === selectedOption.id)) {
      setFunction([...currentList, selectedOption]);
      setIdFunction((prevIds) => [...prevIds, selectedOption.id]);
    }
  };

  const handleRemoveOption = (
    id: number,
    setFunction: React.Dispatch<React.SetStateAction<DropdownOption[]>>,
    setIdFunction: React.Dispatch<React.SetStateAction<number[]>>,
    currentList: DropdownOption[]
  ) => {
    setFunction(currentList.filter((item) => item.id !== id));
    setIdFunction((prevIds) => prevIds.filter((itemId) => itemId !== id));
  };

  return (
    <div className="profile-page">
      <h1 className="profile-title">Profile</h1>
      <div className="profile-section">
        <label className="profile-label">Username</label>
        <input
          className="profile-input"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
        />
      </div>
      <div className="profile-section">
        <label className="profile-label">Email</label>
        <input
          className="profile-input"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </div>
      <div className="profile-section">
        <h3 className="profile-subtitle">Artists</h3>
        <ul className="profile-list">
          {artists.map((artist) => (
            <li key={artist.id} className="profile-list-item">
              {artist.name}
              <button
                className="remove-button"
                onClick={() => handleRemoveOption(artist.id, setArtists, setArtistIds, artists)}
              >
                Remove
              </button>
            </li>
          ))}
        </ul>
        <Select
          options={allArtists.map((artist) => ({ value: artist.id, label: artist.name }))}
          onChange={(option) =>
            handleAddOption({ id: (option as any).value, name: (option as any).label }, setArtists, setArtistIds, artists)
          }
          placeholder="Add Artist"
        />
      </div>
      <div className="profile-section">
        <h3 className="profile-subtitle">Genres</h3>
        <ul className="profile-list">
          {genres.map((genre) => (
            <li key={genre.id} className="profile-list-item">
              {genre.name}
              <button
                className="remove-button"
                onClick={() => handleRemoveOption(genre.id, setGenres, setGenreIds, genres)}
              >
                Remove
              </button>
            </li>
          ))}
        </ul>
        <Select
          options={allGenres.map((genre) => ({ value: genre.id, label: genre.name }))}
          onChange={(option) =>
            handleAddOption({ id: (option as any).value, name: (option as any).label }, setGenres, setGenreIds, genres)
          }
          placeholder="Add Genre"
        />
      </div>
      <div className="profile-section">
        <h3 className="profile-subtitle">Locations</h3>
        <ul className="profile-list">
          {locations.map((location) => (
            <li key={location.id} className="profile-list-item">
              {location.name}
              <button
                className="remove-button"
                onClick={() =>
                  handleRemoveOption(location.id, setLocations, setLocationIds, locations)
                }
              >
                Remove
              </button>
            </li>
          ))}
        </ul>
        <Select
          options={allLocations.map((location) => ({ value: location.id, label: location.name }))}
          onChange={(option) =>
            handleAddOption({ id: (option as any).value, name: (option as any).label }, setLocations, setLocationIds, locations)
          }
          placeholder="Add Location"
        />
      </div>
      <button className="save-button" onClick={handleSave}>
        Save Changes
      </button>
    </div>
  );
};

export default Profile;
