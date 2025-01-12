import React, { useState } from "react";

interface EventPopupProps {
  mode: "add" | "edit";
  initialData: {
    name: string;
    description: string;
    date?: string | Date | null;
    locationId?: number;
    eventTypeId?: number;
    genreId?: number;
    mainArtistId?: number;
    theme?: string;
  };
  dropdownData: {
    artists: { id: number; name: string }[];
    locations: { id: number; name: string }[];
    genres: { id: number; name: string }[];
    eventTypes: { id: number; name: string }[];
  };
  onSave: (data: {
    name: string;
    description: string;
    date?: Date;
    locationId?: number;
    eventTypeId?: number;
    genreId?: number;
    mainArtistId?: number;
    theme?: string;
  }) => void;
  onClose: () => void;
}

const EventPopup: React.FC<EventPopupProps> = ({
  mode,
  initialData,
  dropdownData,
  onSave,
  onClose,
}) => {
  const [name, setName] = useState(initialData.name || "");
  const [description, setDescription] = useState(initialData.description || "");
  const [date, setDate] = useState(
    initialData.date
      ? typeof initialData.date === "string"
        ? initialData.date.split("T")[0] // Handle ISO string
        : (initialData.date as Date).toISOString().split("T")[0] // Handle Date object
      : ""
  );
  const [locationId, setLocationId] = useState(initialData.locationId || "");
  const [eventTypeId, setEventTypeId] = useState(initialData.eventTypeId || "");
  const [genreId, setGenreId] = useState(initialData.genreId || "");
  const [mainArtistId, setMainArtistId] = useState(initialData.mainArtistId || "");
  const [theme, setTheme] = useState(initialData.theme || "");

  const handleSubmit = () => {
    onSave({
      name,
      description,
      date: date ? new Date(date) : undefined,
      locationId: locationId ? Number(locationId) : undefined,
      eventTypeId: eventTypeId ? Number(eventTypeId) : undefined,
      genreId: genreId ? Number(genreId) : undefined,
      mainArtistId: mainArtistId ? Number(mainArtistId) : undefined,
      theme,
    });
    onClose();
  };

  return (
    <div className="popup">
      <div className="popup-content">
        <h3>{mode === "add" ? "Add Event" : "Edit Event"}</h3>
        <input
          type="text"
          placeholder="Event Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <textarea
          placeholder="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <input
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
        />
        <select
          value={locationId}
          onChange={(e) => setLocationId(e.target.value)}
        >
          <option value="">Select Location</option>
          {dropdownData.locations.map((loc) => (
            <option key={loc.id} value={loc.id}>
              {loc.name}
            </option>
          ))}
        </select>
        <select
          value={eventTypeId}
          onChange={(e) => setEventTypeId(e.target.value)}
        >
          <option value="">Select Event Type</option>
          {dropdownData.eventTypes.map((type) => (
            <option key={type.id} value={type.id}>
              {type.name}
            </option>
          ))}
        </select>
        <select value={genreId} onChange={(e) => setGenreId(e.target.value)}>
          <option value="">Select Genre</option>
          {dropdownData.genres.map((genre) => (
            <option key={genre.id} value={genre.id}>
              {genre.name}
            </option>
          ))}
        </select>
        <select
          value={mainArtistId}
          onChange={(e) => setMainArtistId(e.target.value)}
        >
          <option value="">Select Main Artist</option>
          {dropdownData.artists.map((artist) => (
            <option key={artist.id} value={artist.id}>
              {artist.name}
            </option>
          ))}
        </select>
        <input
          type="text"
          placeholder="Theme"
          value={theme}
          onChange={(e) => setTheme(e.target.value)}
        />
        <div className="popup-buttons">
          <button onClick={handleSubmit}>
            {mode === "add" ? "Add" : "Save"}
          </button>
          <button onClick={onClose}>Cancel</button>
        </div>
      </div>
    </div>
  );
};

export default EventPopup;
