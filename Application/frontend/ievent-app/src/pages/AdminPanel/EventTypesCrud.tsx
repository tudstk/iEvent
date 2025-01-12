import React, { useState, useEffect } from "react";
import { getAllEventTypes, deleteEventType, addEventType, updateEventType } from "../../api/eventtype";
import { GetAllEventTypesDto, AddEventTypeDto, UpdateEventTypeDto } from "../../types/dtos/eventtypeDto";
import EventTypePopup from "./EventTypePopup";


const EventTypesCrud: React.FC = () => {
  const [eventTypes, setEventTypes] = useState<GetAllEventTypesDto[]>([]);
  const [popupMode, setPopupMode] = useState<"add" | "edit" | null>(null);
  const [selectedEventType, setSelectedEventType] =
    useState<GetAllEventTypesDto | null>(null);

  useEffect(() => {
    const fetchEventTypes = async () => {
      try {
        const data = await getAllEventTypes();
        setEventTypes(data);
      } catch (error) {
        console.error("Error fetching event types:", error);
      }
    };

    fetchEventTypes();
  }, []);

  const handleDelete = async (id: number) => {
    try {
      await deleteEventType(id);
      setEventTypes(eventTypes.filter((type) => type.id !== id));
    } catch (error) {
      console.error("Error deleting event type:", error);
    }
  };

  const handleSave = async (data: { name: string; description: string }) => {
    try {
      if (popupMode === "add") {
        const newEventType: AddEventTypeDto = {
          name: data.name,
          description: data.description,
        };
        await addEventType(newEventType);
        setEventTypes([
          ...eventTypes,
          { id: eventTypes.length + 1, name: data.name, description: data.description },
        ]);
      } else if (popupMode === "edit" && selectedEventType) {
        const updatedEventType: UpdateEventTypeDto = {
          id: selectedEventType.id,
          name: data.name,
          description: data.description,
        };
        await updateEventType(updatedEventType);
        setEventTypes(
          eventTypes.map((type) =>
            type.id === selectedEventType.id
              ? { ...type, ...data }
              : type
          )
        );
      }
      setPopupMode(null);
      setSelectedEventType(null);
    } catch (error) {
      console.error("Error saving event type:", error);
    }
  };

  return (
    <div className="crud-container">
      <h2>Event Types</h2>
      <button onClick={() => setPopupMode("add")}>Add Event Type</button>
      <ul>
        {eventTypes.map((type) => (
          <li key={type.id}>
            <strong>{type.name}</strong> - {type.description}
            <button
              onClick={() => {
                setSelectedEventType(type);
                setPopupMode("edit");
              }}
            >
              Edit
            </button>
            <button onClick={() => handleDelete(type.id)}>Delete</button>
          </li>
        ))}
      </ul>
      {popupMode && (
        <EventTypePopup
          mode={popupMode}
          initialData={
            popupMode === "edit" && selectedEventType
              ? {
                  name: selectedEventType.name,
                  description: selectedEventType.description || "",
                }
              : { name: "", description: "" }
          }
          onSave={handleSave}
          onClose={() => {
            setPopupMode(null);
            setSelectedEventType(null);
          }}
        />
      )}
    </div>
  );
};

export default EventTypesCrud;
