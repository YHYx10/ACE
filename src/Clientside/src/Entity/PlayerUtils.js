import { Authentication } from '../Authentication/Authentication';
import { Chat } from '../Gui/Chat';
import { Browser } from '../Bridge/Browser';
import { Arena } from '../Client/Arena/Arena';
import { Me } from './Player';

export function isInAnyActivity() {
    return !Authentication.isLoggedin() || Chat.isVisible() || Browser.isOpened() || Arena.isPlayingDeathMatch() || Me.isCuffed() || Me.isInAction();
}
