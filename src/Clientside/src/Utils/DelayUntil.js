/**
 * @param {(...args: any) => void} callback
 * @param {() => boolean} condition
 * @param {number} timeout
 * @param {number} iterations
 *
 * @returns {(...args: any) => void}
 */
export function delayUntil(callback, condition, timeout = 1000, iterations = 10) {
    return (...args) => {
        let counter = 0;

        function check() {
            counter += 1;

            if (condition() || counter > iterations) {
                callback(...args);
            } else {
                setTimeout(check, timeout);
            }
        }

        check();
    };
}
