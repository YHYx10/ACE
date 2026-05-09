let shop24Open = false;

mp.events.add("shop24:open", (data, money)=>
{
	shop24Open = true;
    global.gui.setData("roundTheClockShop/setData", data, money);
    global.gui.openPage("RoundTheClockShop");
});

mp.events.add("shop24:buy", (data, type)=>{
    global.gui.close();
    mp.events.callRemote("shop24:buy", data, type);
});

function closeShop24()
{
	if (!shop24Open) return;
	
	shop24Open = false;
	global.gui.close();
}

mp.events.add("shop24:close", ()=>
{
    closeShop24();
});

mp.keys.bind(global.Keys.Key_ESCAPE, false, function () 
{
    closeShop24();
});