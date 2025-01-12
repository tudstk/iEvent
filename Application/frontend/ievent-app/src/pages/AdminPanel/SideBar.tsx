import React from "react";

interface SidebarProps {
  setSelectedComponent: (component: string) => void;
  isAdmin: boolean;
  isOrganizer: boolean;
}

const Sidebar: React.FC<SidebarProps> = ({ setSelectedComponent, isAdmin, isOrganizer }) => {
  // Admin sees all options; organizer sees only "Events"
  const options = isAdmin
    ? ["Artists", "Events", "EventTypes", "Genres", "Locations"]
    : isOrganizer
    ? ["Events"]
    : [];

    console.log("options: ", options);

  return (
    <div className="sidebar">
      <h3>Categories</h3>
      <ul>
        {options.map((option) => (
          <li key={option} onClick={() => setSelectedComponent(option)}>
            {option}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Sidebar;
