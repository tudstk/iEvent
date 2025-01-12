
import { User } from './User';
import { UserFavoriteGenres } from './UserFavoriteGenres';
import { UserFavoriteArtists } from './UserFavoriteArtists';
import { UserFavoriteLocations } from './UserFavoriteLocations';
import { UserEventHistory } from './UserEventHistory';
import { EventI } from './Event';
import { Location } from './Location';
import { Genre } from './Genre';
import { Artist } from './Artist';

export const dummyUser: User = {
    id: 1,
    name: 'John Doe',
    email: 'john.doe@example.com',
    passwordHash: 'password',
    role: 'admin'
};

export const dummyUserGenres: UserFavoriteGenres[] = [
    { id: 1, userId: 1, genreId: 101 },
    { id: 2, userId: 1, genreId: 102 }
];

export const dummyUserArtists: UserFavoriteArtists[] = [
    { id: 1, userId: 1, artistId: 201 },
    { id: 2, userId: 1, artistId: 202 }
];

export const dummyUserLocations: UserFavoriteLocations[] = [
    { id: 1, userId: 1, locationId: 301 },
    { id: 2, userId: 1, locationId: 302 }
];

export const dummyUserEvents: UserEventHistory[] = [
    { id: 1, userId: 1, eventId: 401, isPreffered: true },
    { id: 2, userId: 1, eventId: 402, isPreffered: false }
];

export const dummyEvents: EventI[] = [
    { id: 401, name: 'MALEVOLENCE / Taking Back August / Take No More - Expirat, Bucharest', date: new Date('2024-12-15'), location: 303, image: 'https://lastfm.freetls.fastly.net/i/u/ar0/ce807dc2f42a3fbb6ec07f1fe790148b', description: 'Malevolence are coming to Bucharest, 15.12 at Expirat! Special guests: Taking Back August & Take No More' },
    { id: 402, name: 'Event 2', date: new Date('2023-10-02'), location: 302 }
];

export const dummyLocations: Location[] = [
    { id: 301, name: 'Location 1', address: 'Address 1', events: [] },
    { id: 302, name: 'Location 2', address: 'Address 2', events: [402] },
    { id: 303, name: 'Expirat', address: 'Address 2', events: [401] },
];

export const dummyArtists: Artist[] = [
    { id: 201, name: 'Artist 1', genre: 101 },
    { id: 202, name: 'Artist 2', genre: 102 },
    { id: 203, name: 'Malevolence', genre: 102 },
    { id: 204, name: 'Taking Back August', genre: 102 },
    { id: 205, name: 'Take No More', genre: 102 },
]

export const dummyGenres: Genre[] = [
    { id: 101, name: 'Genre 1' },
    { id: 102, name: 'Genre 2' },
]