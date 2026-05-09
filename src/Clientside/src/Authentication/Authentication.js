import { Browser } from '../Bridge/Browser';
import { Server } from '../Bridge/Server';

class AuthenticationImpl {
    constructor() {
        this.loggedin = false;
    }

    isLoggedin() {
        return this.loggedin;
    }

    login() {
        this.loggedin = true;
    }

    logout() {
        this.loggedin = false;
    }

    open() {
        Browser.openPage('Auth');

        const plainPassword = mp.storage.data.auth && mp.storage.data.auth.save ? mp.storage.data.auth.plainPassword : '';
        const login = mp.storage.data.auth && mp.storage.data.auth.save ? mp.storage.data.auth.login : '';

        Server.trigger('Auth:PlayerReady', login, plainPassword);
    }
}

export const Authentication = new AuthenticationImpl();
