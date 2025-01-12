import React, { useState } from "react";

interface CrudPopupProps {
  mode: "add" | "edit";
  initialData?: string;
  onSave: (data: string) => void;
  onClose: () => void;
}

const CrudPopup: React.FC<CrudPopupProps> = ({ mode, initialData = "", onSave, onClose }) => {
  const [data, setData] = useState<string>(initialData);

  const handleSubmit = () => {
    onSave(data);
    onClose();
  };

  return (
    <div className="crud-popup">
      <div className="popup-content">
        <h3>{mode === "add" ? "Add Item" : "Edit Item"}</h3>
        <input
          type="text"
          value={data}
          onChange={(e) => setData(e.target.value)}
        />
        <div className="popup-buttons">
          <button onClick={handleSubmit}>{mode === "add" ? "Add" : "Save"}</button>
          <button onClick={onClose}>Cancel</button>
        </div>
      </div>
    </div>
  );
};

export default CrudPopup;
