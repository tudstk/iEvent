export interface EventI {
    id: number;
    name: string;
    date: Date;
    location: number;
    type?: string;
    genre?: number;
    artists?: number[];
    participants?: number[];
    theme?: string;
    description?: string;
    image?: string;
}