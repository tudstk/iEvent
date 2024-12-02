import React, { useState, useEffect } from 'react';
import { EventI } from '../models/Event';
import { dummyEvents, dummyUserEvents, dummyLocations } from '../models/dummy';
import { User } from '../models/User';
import { UserEventHistory } from '../models/UserEventHistory';

const EventPage: React.FC<{ event: EventI, user: User | null }> = ({ event, user }) => {
    const [isAttending, setIsAttending] = useState(false);
    const [events, setEvents] = useState<UserEventHistory[]>([]);
    const [locationName, setLocationName] = useState<string>('');

    useEffect(() => {
        setEvents(dummyUserEvents.filter(e => e.userId === user?.id));
    }, [user?.id]);

    useEffect(() => {
        setIsAttending(events.some(e => e.eventId === event.id));
    }, [events, event.id]);

    useEffect(() => {
        const location = dummyLocations.find(loc => loc.id === event.location);
        if (location) {
            setLocationName(location.name);
        }
    }, [event.location]);

    const handleAttendClick = () => {
        if (isAttending) {
            setEvents(events.filter(e => e.eventId !== event.id));
        } else {
            setEvents([...events, { id: events.length, userId: user!.id, eventId: event.id, isPreffered: false }]);
        }
        setIsAttending(!isAttending);
    };

    const similarEvents = dummyEvents.filter(e => e.genre === event.genre && e.id !== event.id);

    return (
        <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100">
            <div className="w-full max-w-2xl p-8 space-y-6 bg-white rounded-lg shadow-md">
                {event.image && (
                    <img
                        src={event.image}
                        alt={event.name}
                        className="w-full h-64 object-cover rounded-md transition-all duration-300 ease-in-out"
                    />
                )}
                <h1 className="text-2xl font-bold text-center text-gray-800">{event.name}</h1>
                <p className="text-center text-gray-600">{event.date.toDateString()}</p>
                <p className="text-center text-gray-600">{locationName}</p>
                {event.description && <p className="text-gray-700">{event.description}</p>}
                {user != null && <button
                    className={`w-full px-4 py-2 mt-2 text-white rounded-md focus:outline-none focus:ring-2 focus:ring-offset-2 ${isAttending ? 'bg-indigo-600 hover:bg-indigo-700 focus:ring-indigo-500' : 'bg-indigo-600 hover:bg-indigo-700 focus:ring-indigo-500'}`}
                    onClick={handleAttendClick}
                >
                    {isAttending ? 'Remove' : 'Attend'}
                </button>}
                <div>
                    <h2 className="text-xl font-bold text-gray-800">Similar Events</h2>
                    <ul className="list-disc list-inside">
                        {similarEvents.map((similarEvent) => (
                            <li key={similarEvent.id} className="text-gray-700">
                                {similarEvent.name}
                            </li>
                        ))}
                    </ul>
                </div>
                <div>
                    <h2 className="text-xl font-bold text-gray-800">Feedback/Comments</h2>
                    {/* Add feedback/comments section here */}
                </div>
            </div>
        </div>
    );
};

export default EventPage;
