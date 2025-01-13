import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import "./Event.css";
import { getEventById } from "../../api/events";
import { GetByIdEventDto } from "../../types/dtos/eventss";

const Eventt: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [event, setEvent] = useState<GetByIdEventDto | null>(null);

  useEffect(() => {
    const fetchEvent = async () => {
      try {
        if (id) {
          const eventDetails = await getEventById(parseInt(id));
          setEvent(eventDetails);
        }
      } catch (error) {
        console.error("Error fetching event details:", error);
      }
    };

    fetchEvent();
  }, [id]);

  if (!event) {
    return <p className="loading-text">Loading event details...</p>;
  }

  return (
    <div className="event-page">
      <button className="btn btn-back" onClick={() => navigate(-1)}>
        ← Back
      </button>
      <div className="event-details">
        <h1 className="event-title">{event.name}</h1>
        {event.description && <p className="event-description">{event.description}</p>}
        <div className="event-info">
          {event.date && <p><strong>Date:</strong> {new Date(event.date).toLocaleDateString()}</p>}
          {event.locationName && <p><strong>Location:</strong> {event.locationName}</p>}
          {event.eventTypeName && <p><strong>Type:</strong> {event.eventTypeName}</p>}
          {event.genreName && <p><strong>Genre:</strong> {event.genreName}</p>}
          {event.mainArtistName && <p><strong>Main Artist:</strong> {event.mainArtistName}</p>}
          {event.theme && <p><strong>Theme:</strong> {event.theme}</p>}
        </div>
      </div>
    </div>
  );
};

export default Eventt;
