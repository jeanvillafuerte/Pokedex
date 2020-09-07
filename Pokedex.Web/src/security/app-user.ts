export class AppUser {
    user = '';
    password = '';
}

export class AppUserAuth {

    constructor() {
        this.userName = '';
        this.bearerToken = '';
        this.isAuthenticated = false;
        this.id = 0;
    }

    id = 0;
    userName = '';
    bearerToken = '';
    isAuthenticated = false;

}
