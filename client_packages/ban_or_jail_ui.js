mp.events.add('showBanOrJailMessage', (type, reason, duration) => {
    const browser = mp.browsers.new('package://ui/ban_or_jail.html');

    browser.execute(`setBanOrJailMessage('${type}', '${reason}', ${duration});`);

    // Close the browser after 5 seconds
    setTimeout(() => {
        browser.destroy();
    }, 5000);
});
