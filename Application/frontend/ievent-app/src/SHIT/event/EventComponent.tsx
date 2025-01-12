import React from 'react';
import { EventI } from '../models/Event';

const EventComponent: React.FC<{ event: EventI }> = ({ event }) => {
    return (
        <div className="flex flex-col items-center p-4 bg-white rounded-lg shadow-md mb-4">
            {event.image && (
                <img
                    src={event.image}
                    alt={event.name}
                    className="w-full h-32 object-cover rounded-md transition-all duration-300 ease-in-out"
                />
            )}
            <h2 className="text-lg font-bold text-center text-gray-800 mt-2">{event.name}</h2>
            <p className="text-center text-gray-600">{event.date.toDateString()}</p>
            <p className="text-center text-gray-600">{event.location}</p>
        </div>
    );
};

export default EventComponent;
