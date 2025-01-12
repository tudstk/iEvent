import React, { useState, useEffect } from "react";
import LocationPopup from "./LocationPopup";
import { getAllLocations, deleteLocation, addLocation, updateLocation } from "../../api/location";
import { GetAllLocationsDto, AddLocationDto, UpdateLocationDto } from "../../types/dtos/locationdto";


const LocationsCrud: React.FC = () => {
  const [locations, setLocations] = useState<GetAllLocationsDto[]>([]);
  const [popupMode, setPopupMode] = useState<"add" | "edit" | null>(null);
  const [selectedLocation, setSelectedLocation] =
    useState<GetAllLocationsDto | null>(null);

  useEffect(() => {
    const fetchLocations = async () => {
      try {
        const data = await getAllLocations();
        setLocations(data);
      } catch (error) {
        console.error("Error fetching locations:", error);
      }
    };

    fetchLocations();
  }, []);

  const handleDelete = async (id: number) => {
    try {
      await deleteLocation(id);
      setLocations(locations.filter((location) => location.id !== id));
    } catch (error) {
      console.error("Error deleting location:", error);
    }
  };

  const handleSave = async (data: { name: string; description: string }) => {
    try {
      if (popupMode === "add") {
        const newLocation: AddLocationDto = {
          name: data.name,
          description: data.description,
        };
        await addLocation(newLocation);
        setLocations([
          ...locations,
          { id: locations.length + 1, name: data.name, description: data.description },
        ]); // Replace with updated fetch if needed
      } else if (popupMode === "edit" && selectedLocation) {
        const updatedLocation: UpdateLocationDto = {
          id: selectedLocation.id,
          name: data.name,
          description: data.description,
        };
        await updateLocation(updatedLocation);
        setLocations(
          locations.map((location) =>
            location.id === selectedLocation.id
              ? { ...location, ...data }
              : location
          )
        );
      }
      setPopupMode(null);
      setSelectedLocation(null);
    } catch (error) {
      console.error("Error saving location:", error);
    }
  };

  return (
    <div className="crud-container">
      <h2>Locations</h2>
      <button onClick={() => setPopupMode("add")}>Add Location</button>
      <ul>
        {locations.map((location) => (
          <li key={location.id}>
            <strong>{location.name}</strong> - {location.description}
            <button
              onClick={() => {
                setSelectedLocation(location);
                setPopupMode("edit");
              }}
            >
              Edit
            </button>
            <button onClick={() => handleDelete(location.id)}>Delete</button>
          </li>
        ))}
      </ul>
      {popupMode && (
        <LocationPopup
          mode={popupMode}
          initialData={
            popupMode === "edit" && selectedLocation
              ? {
                  name: selectedLocation.name,
                  description: selectedLocation.description || "",
                }
              : { name: "", description: "" }
          }
          onSave={handleSave}
          onClose={() => {
            setPopupMode(null);
            setSelectedLocation(null);
          }}
        />
      )}
    </div>
  );
};

export default LocationsCrud;
