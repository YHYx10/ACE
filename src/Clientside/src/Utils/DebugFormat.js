const MAX_DEPTH = 5;

function isPlainObject(obj) {
    if (!obj || typeof obj !== 'object') {
        return false;
    }

    if (typeof obj.constructor === 'function') {
        return false;
    }

    try {
        if (obj.constructor && !Object.hasOwnProperty.call(obj,
            'constructor',
        ) && !Object.hasOwnProperty.call(obj.constructor.prototype, 'isPrototypeOf')) {
            return false;
        }
    } catch (e) {
        // IE8,9 Will throw exceptions on certain host objects #9897
        return false;
    }

    let key;

    for (key in obj) {
        // noop
    }

    return key === undefined || Object.hasOwnProperty.call(obj, key);
}

function isFunction(obj) {
    return typeof obj === 'function';
}

export function debugFormat(data, pretty = false, depth = 0) {
    try {
        if (depth > MAX_DEPTH) {
            return '[...]';
        }

        const indent = pretty ? '  '.repeat(depth + 1) : '';

        if (data === null) {
            return 'null';
        }

        if (typeof data === 'undefined') {
            return 'undefined';
        }

        if (typeof data === 'string') {
            return `"${data}"`;
        }

        if (typeof data === 'number') {
            return data.toString();
        }

        if (typeof data === 'boolean') {
            return data.toString();
        }

        if (typeof data === 'number' && isNaN(data)) {
            return 'NaN';
        }

        if (data === Infinity) {
            return 'Infinity';
        }

        if (data === -Infinity) {
            return '-Infinity';
        }

        if (isPlainObject(data)) {
            const properties = Object.entries(data)
                .map(([key, value]) => `${key}: ${debugFormat(value, pretty, depth + 1)}`);

            if (pretty) {
                return '{\n' + indent + properties.map(item => indent + item).join(',\n') + '\n}\n';
            } else {
                return '{' + properties.join(', ') + '}';
            }
        }

        if (Array.isArray(data)) {
            const items = data.map((value) => debugFormat(value, pretty, depth + 1));

            if (pretty) {
                return '[\n' + indent + items.map(item => indent + item).join(',\n') + '\n]\n';
            } else {
                return '[' + items.join(', ') + ']';
            }
        }

        if (isFunction(data)) {
            const name = data.name || '[anonymous function]';

            return '[Function ' + name + ']';
        }

        const name = data.constructor ? data.constructor.name : 'Object';
        const prototype = Object.getPrototypeOf(data);

        const properties = Object.entries(data)
            .map(([key, value]) => `${key}: ${debugFormat(value, pretty, depth + 1)}`);

        let result = `${name} {`;

        // Traverse prototype chain for methods
        let currentPrototype = prototype;
        while (currentPrototype && currentPrototype !== Object.prototype) {
            const parentMethodNames = Object.getOwnPropertyNames(currentPrototype)
                .filter(key => typeof currentPrototype[key] === 'function' && key !== 'constructor');

            const parentMethods = parentMethodNames.map((key) => `${key}: [Function ${key}]`);

            if (pretty && parentMethods.length > 0) {
                result += '\n' + indent + parentMethods.join(',\n' + indent);
            } else if (parentMethods.length > 0) {
                result += parentMethods.join(', ');
            }

            currentPrototype = Object.getPrototypeOf(currentPrototype);
        }

        if (pretty) {
            if (properties.length > 0) {
                result += '\n' + indent + properties.join(',\n' + indent);
            }
            result += '\n';
        } else {
            result += properties.join(', ');
        }

        result += '}';

        return result;
    } catch (e) {
        return '[Error: ' + e.message + ']';
    }
}
