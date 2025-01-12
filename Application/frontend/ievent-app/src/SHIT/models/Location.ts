export interface Location {
    id: number;
    name: string;
    address: string;
    city?: string;
    country?: string;
    capacity?: number;
    latitude?: number;
    longitude?: number;
    events: number[];
    image?: string;
    description?: string;
    rating?: number;
    reviews?: number[];
}