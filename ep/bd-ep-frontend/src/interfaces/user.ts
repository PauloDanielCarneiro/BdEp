export interface IUser {
    id?: string
    name?: string,
    email?: string,
    document: string;
    token: string;
}

export interface IGetUserResponse {
    id: string
    name: string,
    email: string,
}