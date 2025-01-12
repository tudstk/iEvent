import React, { useState, useEffect } from "react";
import EventPopup from "./EventPopup";
import { getAllArtists } from "../../api/artists";
import { getAllEvents, deleteEvent, addEvent, updateEvent, getEventById } from "../../api/events";
import { getAllEventTypes } from "../../api/eventtype";
import { getAllGenres } from "../../api/genres";
import { getAllLocations } from "../../api/location";
import { GetAllEventsDto, GetByIdEventDto, AddEventDto, UpdateEventDto } from "../../types/dtos/eventss";


const EventsCrud: React.FC = () => {
  const [events, setEvents] = useState<GetAllEventsDto[]>([]);
  const [popupMode, setPopupMode] = useState<"add" | "edit" | null>(null);
  const [selectedEventId, setSelectedEventId] = useState<number | null>(null);

  // Store dropdown data
  const [artists, setArtists] = useState<{ id: number; name: string }[]>([]);
  const [locations, setLocations] = useState<{ id: number; name: string }[]>([]);
  const [genres, setGenres] = useState<{ id: number; name: string }[]>([]);
  const [eventTypes, setEventTypes] = useState<{ id: number; name: string }[]>([]);

  // Pre-fill data for editing
  const [editData, setEditData] = useState<GetByIdEventDto | null>(null);

  useEffect(() => {
    const fetchInitialData = async () => {
      try {
        const [eventsData, artistsData, locationsData, genresData, eventTypesData] =
          await Promise.all([
            getAllEvents(),
            getAllArtists(),
            getAllLocations(),
            getAllGenres(),
            getAllEventTypes(),
          ]);

        setEvents(eventsData);
        setArtists(artistsData);
        setLocations(locationsData);
        setGenres(genresData);
        setEventTypes(eventTypesData);
      } catch (error) {
        console.error("Error fetching initial data:", error);
      }
    };

    fetchInitialData();
  }, []);

  const handleDelete = async (id: number) => {
    try {
      await deleteEvent(id);
      setEvents(events.filter((event) => event.id !== id));
    } catch (error) {
      console.error("Error deleting event:", error);
    }
  };

  const handleSave = async (data: { name: string; description: string; date?: Date; locationId?: number; eventTypeId?: number; genreId?: number; mainArtistId?: number; theme?: string }) => {
    try {
      if (popupMode === "add") {
        await addEvent(data as AddEventDto);
        setEvents(await getAllEvents()); // Update the events list after creation
      } else if (popupMode === "edit" && selectedEventId) {
        await updateEvent({ ...data, id: selectedEventId });
        setEvents(await getAllEvents()); // Update the events list after editing
      }
      setPopupMode(null);
      setSelectedEventId(null);
      setEditData(null);
    } catch (error) {
      console.error("Error saving event:", error);
    }
  };

  const handleEdit = async (id: number) => {
    try {
      const eventData = await getEventById(id);
      setEditData(eventData);
      setSelectedEventId(id);
      setPopupMode("edit");
    } catch (error) {
      console.error("Error fetching event details:", error);
    }
  };

  return (
    <div className="crud-container">
      <h2>Events</h2>
      <button onClick={() => setPopupMode("add")}>Add Event</button>
      <ul>
        {events.map((event) => (
          <li key={event.id}>
            <strong>{event.name}</strong> - {event.description}
            <button onClick={() => handleEdit(event.id)}>Edit</button>
            <button onClick={() => handleDelete(event.id)}>Delete</button>
          </li>
        ))}
      </ul>
      {popupMode && (
        <EventPopup
          mode={popupMode}
          initialData={
            popupMode === "edit" && editData
              ? {
                  name: editData.name,
                  description: editData.description || "",
                  date: editData.date,
                  locationId: editData.locationName
                    ? locations.find((loc) => loc.name === editData.locationName)?.id
                    : undefined,
                  eventTypeId: editData.eventTypeName
                    ? eventTypes.find((type) => type.name === editData.eventTypeName)?.id
                    : undefined,
                  genreId: editData.genreName
                    ? genres.find((genre) => genre.name === editData.genreName)?.id
                    : undefined,
                  mainArtistId: editData.mainArtistName
                    ? artists.find((artist) => artist.name === editData.mainArtistName)?.id
                    : undefined,
                  theme: editData.theme || "",
                }
              : {
                  name: "",
                  description: "",
                  date: undefined,
                  locationId: undefined,
                  eventTypeId: undefined,
                  genreId: undefined,
                  mainArtistId: undefined,
                  theme: "",
                }
          }
          dropdownData={{ artists, locations, genres, eventTypes }}
          onSave={handleSave}
          onClose={() => {
            setPopupMode(null);
            setSelectedEventId(null);
            setEditData(null);
          }}
        />
      )}
    </div>
  );
};

export default EventsCrud;
