import React, { useState } from 'react';
import { dummyEvents, dummyUser } from '../../SHIT/models/dummy';
import { User } from '../../SHIT/models/User';
import AuthInput from '../../SHIT/auth/AuthInput';
import EventComponent from '../../SHIT/event/EventComponent';
import EventPage from '../../SHIT/event/EventPage';
import Panel from '../../SHIT/user/Panel';
import { extractJwtPayload } from '../../utils/jwtUtils';
import { Link } from 'react-router-dom';


const Home: React.FC = () => {
    const [user, setUser] = useState<User | null>(dummyUser);
    const [showPanel, setShowPanel] = useState(false);
    const [showLogin, setShowLogin] = useState(false);
    const [selectedEvent, setSelectedEvent] = useState<number | null>(null);

    const token = localStorage.getItem('token');
    const tokenInfo = extractJwtPayload(token || '');

    const handleLogin = () => {
        setUser(dummyUser);
        setShowLogin(false);
    };

    const handleLogout = () => {
        setUser(null);
        setShowPanel(false);
        setSelectedEvent(null);
    };

    const handleShowPanel = () => {
        setShowPanel(true);
        setSelectedEvent(null);
    };

    const handleShowLogin = () => {
        setShowLogin(true);
        setShowPanel(false);
        setSelectedEvent(null);
    };

    const handleShowEvent = (eventId: number) => {
        setSelectedEvent(eventId);
        setShowPanel(false);
        setShowLogin(false);
    };

    return (
        <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100">
          {(tokenInfo?.isAdmin === "true" || tokenInfo?.isOrganizer === "true") && <button className="text-2xl font-bold text-center text-gray-800"><Link to="/admin-panel">Admin Panel</Link></button>}
            {!showPanel && !showLogin && selectedEvent === null && (
                <div className="w-full max-w-2xl p-8 space-y-6 bg-white rounded-lg shadow-md">
                    <h1 className="text-2xl font-bold text-center text-gray-800">Home Page</h1>
                    {user ? (
                        <div className="text-center">
                            <p className="text-gray-700">Welcome, {user.name}!</p>
                            <button onClick={handleLogout} className="ml-4 text-red-600 hover:underline">Logout</button>
                            <button onClick={handleShowPanel} className="ml-4 text-indigo-600 hover:underline">Go to User Panel</button>
                        </div>
                    ) : (
                        <div className="text-center">
                            <p className="text-gray-700">You are not logged in.</p>
                            <button onClick={handleShowLogin} className="ml-4 text-indigo-600 hover:underline">Login as Dummy User</button>
                        </div>
                    )}
                    <h2 className="text-xl font-bold text-gray-800">Event Feed</h2>
                    <div className="space-y-4">
                        {dummyEvents.map(event => (
                            <div key={event.id} onClick={() => handleShowEvent(event.id)}>
                                <EventComponent event={event} />
                            </div>
                        ))}
                    </div>
                </div>
            )}
            {showPanel && user && <Panel user={user} />}
            {showLogin && <AuthInput />}
            {selectedEvent !== null && (
                <EventPage event={dummyEvents.find(e => e.id === selectedEvent)!} user={user} />
            )}
        </div>
    );
};

export default Home;