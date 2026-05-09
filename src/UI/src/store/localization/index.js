import * as de from './translate/de.json';
import * as en from './translate/en.json';

export default {
    namespaced: true,
    state: {
        local: null,
        apiURL: 'http://localhost/api/localize/',
        listUrl: 'http://localhost/api/localize/list',
        langs: {
            list: null,
            cur: 'no'
        }
    },
    actions: {
        translateAction({getters}, {id, msg}) {
            window.mp.trigger('tag:add:action', id, getters['loc'](msg));
        },

        setLang({state, dispatch}, lang) {
            window.mp.trigger('language:save', lang);
            if (lang === state.langs.cur) {
                return;
            }
            state.langs.cur = lang;
            //document.getElementsByTagName("html")[0].lang = state.langs.cur;
            dispatch('loadLanguage');
        },

        async loadLanguage({state, dispatch}) {
            // state.local = lang;
            switch (state.langs.cur) {
                case 'de':
                    state.local = de;
                    break;
                case 'en':
                    state.local = en;
                    break;
                default:
                    state.local = en;
            }

            if (state.langs.cur === 'no') {
                setTimeout(() => {
                    dispatch('loadLanguage');
                }, 1000);
            }
        },
        async loadLangs({state}) {
            state.langs.list = {
                'de': 'German',
                'en': 'English',
            };
        }
    },
    modules: {},
    getters: {
        loc: state => msg => {
            if (state.local === null || typeof msg !== 'string') {
                return msg;
            }
            const array = msg.split('@');
            let key = array[0];

            if (state.local[key] === undefined) {
                if (process.env.WEBPACK_SERVE !== 'false') {
                    window.console.log(`Key ${key} don't have value`);
                }

                return key;
            } else {
                let message = state.local[key];
                const params = array.slice(1);
                if (params.length > 0) {
                    for (let index = 0; index < params.length; index++) {
                        const param = params[index];
                        if (message.indexOf(`{${index}}`) === -1) {
                            message += ` ${state.local[param] ? state.local[param] : param}`;
                        } else {
                            message = message.replace(`{${index}}`, state.local[param] ? state.local[param] : param);
                        }
                    }
                }
                return message;
            }
        },

        font(state) {
            switch (state.langs.cur) {
                case 'ge':
                    return 'Georgian';
                default:
                    return 'Russian';
            }
        }
    }
};