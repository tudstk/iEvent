import React, { useState, useEffect } from 'react';
import { User } from '../models/User';
import { UserFavoriteGenres } from '../models/UserFavoriteGenres';
import { UserFavoriteArtists } from '../models/UserFavoriteArtists';
import { UserFavoriteLocations } from '../models/UserFavoriteLocations';
import { UserEventHistory } from '../models/UserEventHistory';
import { dummyArtists, dummyEvents, dummyGenres, dummyLocations, dummyUserGenres, dummyUserArtists, dummyUserEvents, dummyUserLocations } from '../models/dummy';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark, faHeart as solidHeart } from '@fortawesome/free-solid-svg-icons';
import { faHeart as regularHeart } from '@fortawesome/free-regular-svg-icons';
import { EventI } from '../models/Event';

const Panel: React.FC<{ user: User }> = ({ user }) => {
    const [name, setName] = useState(user.name);
    const [email, setEmail] = useState(user.email);
    const [genres, setGenres] = useState<UserFavoriteGenres[]>([]);
    const [artists, setArtists] = useState<UserFavoriteArtists[]>([]);
    const [locations, setLocations] = useState<UserFavoriteLocations[]>([]);
    const [events, setEvents] = useState<UserEventHistory[]>([]);

    useEffect(() => {
        setGenres(dummyUserGenres.filter(g => g.userId === user.id));
        setArtists(dummyUserArtists.filter(a => a.userId === user.id));
        setLocations(dummyUserLocations.filter(l => l.userId === user.id));
        setEvents(dummyUserEvents.filter(e => e.userId === user.id));
    }, [user.id]);

    const handleNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setName(e.target.value);
    };

    const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setEmail(e.target.value);
    };

    const handleAddGenre = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const genreId = parseInt(e.target.value);
        if (!genres.some(g => g.genreId === genreId)) {
            setGenres([...genres, { id: genres.length, userId: user.id, genreId }]);
        }
    };

    const handleAddArtist = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const artistId = parseInt(e.target.value);
        if (!artists.some(a => a.artistId === artistId)) {
            setArtists([...artists, { id: artists.length, userId: user.id, artistId }]);
        }
    };

    const handleAddLocation = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const locationId = parseInt(e.target.value);
        if (!locations.some(l => l.locationId === locationId)) {
            setLocations([...locations, { id: locations.length, userId: user.id, locationId }]);
        }
    };

    const handleDeleteGenre = (id: number) => {
        setGenres(genres.filter(g => g.id !== id));
    };

    const handleDeleteArtist = (id: number) => {
        setArtists(artists.filter(a => a.id !== id));
    };

    const handleDeleteLocation = (id: number) => {
        setLocations(locations.filter(l => l.id !== id));
    };

    const handleTogglePreferred = (eventId: number) => {
        setEvents(events.map(e => e.eventId === eventId ? { ...e, isPreffered: !e.isPreffered } : e));
    };

    const availableGenres = dummyGenres.filter(g => !genres.some(ug => ug.genreId === g.id));
    const availableArtists = dummyArtists.filter(a => !artists.some(ua => ua.artistId === a.id));
    const availableLocations = dummyLocations.filter(l => !locations.some(ul => ul.locationId === l.id));
    const confirmedEvents = dummyEvents.filter(e => events.some(ue => ue.eventId === e.id));

    return (
        <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100">
            <div className="w-full max-w-2xl p-8 space-y-6 bg-white rounded-lg shadow-md">
                <h1 className="text-2xl font-bold text-center text-gray-800">User Panel</h1>
                <div>
                    <label className="block text-sm font-medium text-gray-700">
                        Name:
                        <input
                            type="text"
                            value={name}
                            onChange={handleNameChange}
                            className="w-full px-3 py-2 mt-1 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
                        />
                    </label>
                </div>
                <div>
                    <label className="block text-sm font-medium text-gray-700">
                        Email:
                        <input
                            type="email"
                            value={email}
                            onChange={handleEmailChange}
                            className="w-full px-3 py-2 mt-1 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
                        />
                    </label>
                </div>
                <div>
                    <label className="block text-sm font-medium text-gray-700">
                        Genres:
                        {availableGenres.length > 0 && <select onChange={handleAddGenre} className="w-full px-3 py-2 mt-1 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500">
                            <option value="">Add Genre</option>
                            {availableGenres.map(g => (
                                <option key={g.id} value={g.id}>{g.name}</option>
                            ))}
                        </select>}
                    </label>
                    <ul className="list-disc list-inside">
                        {genres.map((genre) => (
                            <li key={genre.id} className="text-gray-700">
                                {dummyGenres.find(g => g.id === genre.genreId)?.name}
                                <button onClick={() => handleDeleteGenre(genre.id)} className="ml-2 text-red-500">
                                    <FontAwesomeIcon icon={faXmark} />
                                </button>
                            </li>
                        ))}
                    </ul>
                </div>
                <div>
                    <label className="block text-sm font-medium text-gray-700">
                        Artists:
                        {availableArtists.length > 0 && <select onChange={handleAddArtist} className="w-full px-3 py-2 mt-1 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500">
                            <option value="">Add Artist</option>
                            {availableArtists.map(a => (
                                <option key={a.id} value={a.id}>{a.name}</option>
                            ))}
                        </select>}
                    </label>
                    <ul className="list-disc list-inside">
                        {artists.map((artist) => (
                            <li key={artist.id} className="text-gray-700">
                                {dummyArtists.find(a => a.id === artist.artistId)?.name}
                                <button onClick={() => handleDeleteArtist(artist.id)} className="ml-2 text-red-500">
                                    <FontAwesomeIcon icon={faXmark} />
                                </button>
                            </li>
                        ))}
                    </ul>
                </div>
                <div>
                    <label className="block text-sm font-medium text-gray-700">
                        Favorite Locations:
                        {availableLocations.length > 0 && <select onChange={handleAddLocation} className="w-full px-3 py-2 mt-1 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500">
                            <option value="">Add Location</option>
                            {availableLocations.map(l => (
                                <option key={l.id} value={l.id}>{l.name}</option>
                            ))}
                        </select>}
                    </label>
                    <ul className="list-disc list-inside">
                        {locations.map((location) => (
                            <li key={location.id} className="text-gray-700">
                                {dummyLocations.find(l => l.id === location.locationId)?.name}
                                <button onClick={() => handleDeleteLocation(location.id)} className="ml-2 text-red-500">
                                    <FontAwesomeIcon icon={faXmark} />
                                </button>
                            </li>
                        ))}
                    </ul>
                </div>
                <div>
                    <h2 className="text-xl font-bold text-gray-800">Events</h2>
                    <ul className="list-disc list-inside">
                        {confirmedEvents.map((event: EventI, index: number) => (
                            <li key={index} className="text-gray-700">
                                {confirmedEvents.find(e => e.id === event.id)?.name}
                                <button onClick={() => handleTogglePreferred(event.id)} className="ml-2 text-red-500">
                                    <FontAwesomeIcon icon={events.find(e => e.eventId === event.id)?.isPreffered ? solidHeart : regularHeart} />
                                </button>
                            </li>
                        ))}
                    </ul>
                </div>
                {(user.role === 'admin') && (
                    <div>
                        <h2 className="text-xl font-bold text-gray-800">Admin Panel</h2>
                        <button className="w-full px-4 py-2 mt-2 text-white bg-indigo-600 rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
                            Give Roles
                        </button>
                        <button className="w-full px-4 py-2 mt-2 text-white bg-indigo-600 rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
                            Block Users
                        </button>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Panel;