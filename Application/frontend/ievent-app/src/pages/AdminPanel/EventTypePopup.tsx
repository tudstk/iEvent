import React, { useState } from "react";

interface EventTypePopupProps {
  mode: "add" | "edit";
  initialData: { name: string; description: string };
  onSave: (data: { name: string; description: string }) => void;
  onClose: () => void;
}

const EventTypePopup: React.FC<EventTypePopupProps> = ({
  mode,
  initialData,
  onSave,
  onClose,
}) => {
  const [name, setName] = useState(initialData.name);
  const [description, setDescription] = useState(initialData.description);

  const handleSubmit = () => {
    onSave({ name, description });
    onClose();
  };

  return (
    <div className="event-type-popup">
      <div className="popup-content">
        <h3>{mode === "add" ? "Add Event Type" : "Edit Event Type"}</h3>
        <input
          type="text"
          placeholder="Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <textarea
          placeholder="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
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

export default EventTypePopup;
