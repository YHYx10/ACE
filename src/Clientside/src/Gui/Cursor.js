import { Authentication } from '../Authentication/Authentication';

class CursorImpl {
    show() {
        this.setVisibility(true);
    }

    hide() {
        // Only allow to hide the cursor if the user is logged in
        this.setVisibility(!Authentication.isLoggedin());
    }

    setVisibility(visible) {
        mp.gui.cursor.visible = visible;
    }

    isVisible() {
        return mp.gui.cursor.visible;
    }
}

export const Cursor = new CursorImpl();
