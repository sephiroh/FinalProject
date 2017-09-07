export class UserToken {
    id: number;
    firstname: string;
    lastname: string;
    email: string;
    role: string;
    permissions: Array<string>;
    errors: string;
}