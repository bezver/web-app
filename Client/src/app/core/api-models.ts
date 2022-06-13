export interface User {
    id?: string,
    email: string;
    password?: string;
    roles?: string[];
}

export interface Country {
    id?: string;
    name: string;
}

export interface Genre {
    id?: string;
    name: string;
}

export interface Film {
    id?: string;
    name: string;
    description: string;
    releaseDate: string;
    countries?: Country[];
    genres?: Genre[];
    rank?: number;
    myMark?: number;
}

export interface FilmFilter {
    skip: number | undefined;
    take: number | undefined;
}
