let tag = document.createElement('script');
tag.src = 'https://www.youtube.com/iframe_api';
let firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

var player2;
let loaded = false;
let paused = false;
let musics = Object.values(Musics);
let audio = null;
let audioId = -1;
let audioTime = 0;
let audioInterval = null;
let keysOpen = false;

function onYouTubeIframeAPIReady() {
    if (localStorage.getItem("gen_loading-url")) {
        loaded = true;
        player2 = new YT.Player('youtube-player-2', {
            events: {
                onReady: onPlayerReady(localStorage.getItem("gen_loading-url")),
            },
        });
    }
}

function onPlayerReady(url) {
    setTimeout(() => {
        player2.loadVideoByUrl(url)

        setTimeout(() => {
            $("#music-name").text(player2.playerInfo.videoData.title)
            $("#music-author").text(player2.playerInfo.videoData.author)
            player2.playVideo();
        }, 500);

        if (audioInterval) clearInterval(audioInterval);

        $('.volume-input').val(30);
        $('.volume-text').text("30%");
        $(".bar-bg-2").css("width", "30%");

        audioInterval = setInterval(() => {
            $('#music-time-text').text(secondsToDuration(player2.getCurrentTime()));
            $('.end-time').text(secondsToDuration(player2.getDuration()));
            $('#music-time').val(convertValue(player2.getCurrentTime(), 0, player2.getDuration(), 0, 100));
            $('#input-bg-time').width(convertValue(player2.getCurrentTime(), 0, player2.getDuration(), 0, 100) + "%");
        }, 500);

    }, 1000);
}

const convertValue = (value, oldMin, oldMax, newMin, newMax) => {
    const oldRange = oldMax - oldMin
    const newRange = newMax - newMin
    const newValue = ((value - oldMin) * newRange) / oldRange + newMin
    return newValue
}

$("#scroll-l-gallery").click(() => {
    let scroll = $(".gallery-wrapper .scroll");
    let last = scroll.find(".gallery-box").last();
    last.prependTo(scroll);
});

$("#scroll-r-gallery").click(() => {
    let scroll = $(".gallery-wrapper .scroll");
    let first = scroll.find(".gallery-box").first();
    first.appendTo(scroll);
});

$(document).on("click", ".gallery-box", function(){
    let src = $(this).find("img").attr("src");
    $(".img-preview img").attr("src", src);
    $(".img-preview").fadeIn(200);
    $(".img-preview").css("display", "flex");
});

$(".img-preview").click(function(){
    $(this).fadeOut(200);
});

$("#scroll-l-ann").click(() => {
    let scroll = $(".announce-wrapper .scroll");

    let last = scroll.find(".announce").last();
    let width = last.outerWidth();
    scroll.css("margin-left", -width);
    last.prependTo(scroll);
    scroll.animate({marginLeft: 0}, 300);
});

$("#scroll-r-ann").click(() => {
    let scroll = $(".announce-wrapper .scroll");

    let first = scroll.find(".announce").first();
    let width = first.outerWidth();
    scroll.animate({marginLeft: -width}, 300, () => {
        first.appendTo(scroll);
        scroll.css("margin-left", 0);
    });
});

$("#scroll-l-team").click(() => {
    let scroll = $(".authors-wrapper .scroll");

    let last = scroll.find(".author").last();
    let width = last.outerWidth();
    scroll.css("margin-left", -width);
    last.prependTo(scroll);
    scroll.animate({marginLeft: 0}, 200);
});

$("#scroll-r-team").click(() => {
    let scroll = $(".authors-wrapper .scroll");

    let first = scroll.find(".author").first();
    let width = first.outerWidth();
    scroll.animate({marginLeft: -width}, 200, () => {
        first.appendTo(scroll);
        scroll.css("margin-left", 0);
    });
});

$("#keysbutton").click(() => {
    keysOpen = !keysOpen;
    $(".keyboard-section").css("display", keysOpen ? "flex" : "none");
});

$(".time-input").on("input", function() {
    let value = $(this).val();
    $(".bar-bg").css("width", value+"%");

    if (audio) {
        audio.currentTime = value / 100 * audio.duration;
    }else if (localStorage.getItem("gen_loading-url") && !paused){
        player2.seekTo(value / 100 * player2.getDuration());
    }
});

$(".volume-input").on("input", function() {
    let value = $(this).val();
    $(".bar-bg-2").css("width", value+"%");
    $(".volume-text").text(value+"%")

    if (localStorage.getItem("gen_loading-url") && !paused) {
        player2.setVolume(value);
    }else if (audio){
        audio.volume = value / 100; 
    }
});

$("#closeall").click(() => {
    $(".left").fadeToggle(300)
    $(".right").fadeToggle(300)
    $(".loading-bar").fadeToggle(300)
});

$("#save-btn").click(() => {
    let url = $("#url").val();

    if (!url || url == "") return;
    if (!isValidYouTubeUrl(url)) return;

    if (audio){
        audio.pause();
    }

    url = url.replace("https://www.youtube.com/watch?v=", "https://www.youtube.com/embed/");
    localStorage.setItem("gen_loading-url", url);

    if (!loaded && !player2) {
        player2 = new YT.Player('youtube-player-2', {
            events: {
                onReady: onPlayerReady(url),
            },
        });
    }else{
        onPlayerReady(url)
    }

    paused = false;
});

$("#pause-play-video").click(() => {
    $("#youtube-player").toggle()
});

document.onkeyup = function(data){
    if (data.which == 27){
        keysOpen = false;
        $(".keyboard-section").css("display", "none");
    }
};

$(document).ready(function() {
    $("main").removeClass("aqua");
    $("main").removeClass("yellow");
    $("main").removeClass("blue");
    $("main").removeClass("purple");
    $("main").removeClass("red");

    $("main").addClass(Color);

    if(Color == "aqua"){
        $(".startcolor").attr("stop-color", "#58FFF5")
        $(".stopcolor").attr("stop-color", "#9AFFF9")
    }else if(Color == "yellow"){
        $(".startcolor").attr("stop-color", "#FFE073")
        $(".stopcolor").attr("stop-color", "#FFCF27")
    }else if(Color == "blue"){
        $(".startcolor").attr("stop-color", "#73B3FF")
        $(".stopcolor").attr("stop-color", "#1E85FF")
    }else if(Color == "purple"){
        $(".startcolor").attr("stop-color", "#A873FF")
        $(".stopcolor").attr("stop-color", "#873EFF")
    }else if(Color == "red"){
        $(".startcolor").attr("stop-color", "#FF7373")
        $(".stopcolor").attr("stop-color", "#FF3838")
    }
        

    $(document).on("click", ".social-box.discord", () => {
        window.invokeNative("openUrl", Socials.discord);
    });

    $(document).on("click", ".social-box.instagram", () => {
        window.invokeNative("openUrl", Socials.instagram);
    });

    $(document).on("click", ".social-box.youtube", () => {
        window.invokeNative("openUrl", Socials.youtube);
    });

    Gallery.forEach(image => {
        $(".gallery-wrapper .scroll").append(`
        <div class="gallery-box">
            <div class="img">
                <img src="assets/gallery/${image}">
            </div>
        </div>
        `)
    });

    Rules.forEach(rule => {
        $(".rules-wrapper").append(`
        <div class="rule">
            <div class="title">${rule.title}</div>

            <div class="desc-wrapper">
                <div class="title-2">${rule.title}</div>
                <div class="desc">${rule.description}</div>
            </div>
        </div>
        `)
    });

    Announces.forEach(announce => {
        $(".announce-wrapper .scroll").append(`
        <div class="announce">
            <div class="image"><img src="assets/announce/${announce.image}"></div>
            <div class="desc">${announce.description}</div>
            <div class="title">${announce.title}</div>
        </div>
        `)
    });

    Authors.forEach(author => {
        $(".authors-wrapper .scroll").append(`
        <div class="author">
            <div class="image"><img src="assets/authors/${author.image}"></div>
            <div class="name">${author.name}</div>
            <div class="rank">${author.rank}</div>
        </div>
        `)
    });

    Informations.forEach(inform => {
        $(".information-wrapper").append(`
        <div class="inform">
            <div class="title">${inform.title}</div>
            <div class="desc">${inform.description}</div>
        </div>
        `)
    });

    Keys.forEach(key => {
        $(".keys-wrapper").append(`
        <div class="key-inform">
            <div class="key">${key.key}</div>
            <div class="title-wrapper">
                <div class="title">${key.key} KEY</div>
                <div class="desc">${key.description}</div>
            </div>
        </div>
        `)

        $(`.row .key[data-key='${key.key}']`).addClass("active")
    });

    playAudio(0)

    $('#play').click(() => resumeAudio());
    $('#pause').click(() => pauseAudio());
    $('#next').click(() => nextSong());
    $('#prev').click(() => nextSong(true));
    $('#first').click(() => seek());
    $('#last').click(() => seek(true));

    $('#music-time').on('change', function () {
        audio.seek(convertValue($(this).val(), 0, 100, 0, audio.duration()));
    });

    $(window).on("message", function ({ originalEvent: e }) {
        switch (e.data.eventName) {
            case 'loadProgress':
                $(".loading-bar .text").css("left", e.data.loadFraction * 100+"%")
                $(".loading-bar .text").css("transform", "translateX(-"+e.data.loadFraction * 100+"%)")
                $(".loading-bar .percent").text("%"+(e.data.loadFraction * 100).toFixed())
                $(".will-load").attr("width", convertValue(e.data.loadFraction*100, 0, 100, 0, 1373))
                $(".will-load").attr("viewBox", "0 0 " + convertValue(e.data.loadFraction*100, 0, 100, 0, 1373) + " 100")
                break;
        }
    })
})

function secondsToDuration(sec) {
    return `${Math.floor(sec / 60)
        .toString()
        .padStart(2, '0')}:${Math.round(sec % 60)
        .toString()
        .padStart(2, '0')}`;
}

function playAudio(id) {
    stopAudio();

    if (!localStorage.getItem("gen_loading-url") || paused) {
        const music = musics[id];

        if (music) {
            audio = new Audio(music.audio);
            audio.volume = 0.5;
            $('.volume-input').val(30);
            $('.volume-text').text("30%");
            $(".bar-bg-2").css("width", "30%");
    
            resumeAudio();

            if (audioInterval) clearInterval(audioInterval);
            
            audioId = id;
            audioInterval = setInterval(() => {
                $('#music-time-text').text(secondsToDuration(audio.currentTime));
                $('.end-time').text(secondsToDuration(audio.duration));
                $('#music-time').val(convertValue(audio.currentTime, 0, audio.duration, 0, 100));
                $('#input-bg-time').width(convertValue(audio.currentTime, 0, audio.duration, 0, 100) + "%");
            }, 500);
    
            $('#music-name').text(music.title);
            $('#music-author').text(music.author);
            $('#music-image').attr("src", music.image);
        }
    }
}

function stopAudio() {
    if (audio) {
        audio.pause();

        audio = null;
        audioId = -1;

        $('#music-name').text('Not Playing');
    }
}

function pauseAudio() {
    if (audio) {
        audio.pause();

        $('#play').show();
        $('#pause').hide();
    }

    if (localStorage.getItem("gen_loading-url") && !paused) {
        if (player2.getPlayerState() == 1) {
            player2.pauseVideo();

            $('#play').show();
            $('#pause').hide();
        }
    }
}

function resumeAudio() {
    if (localStorage.getItem("gen_loading-url") && !paused) {
        player2.playVideo();

        $('#play').hide();
        $('#pause').show();
    }else{
        if (audio) {
            audio.play();
    
            $('#play').hide();
            $('#pause').show();
        }else{
            playAudio(0);
        }
    }
}

function nextSong(prev) {
    if (localStorage.getItem("gen_loading-url") && !paused) {
        localStorage.removeItem("gen_loading-url");

        if (player2.getPlayerState() == 1) {
            player2.pauseVideo();
        }
    }

    paused = true;

    if (audio) {
        prev ? audioId-- : audioId++;

        if (audioId >= musics.length) audioId = 0;
        else if (audioId < 0) audioId = musics.length - 1;

        playAudio(audioId);
    }else{
        console.log("playAudio(0)")
        playAudio(0);
    }
}

function seek(last) {
    if (audio) {
        last ? audioId = musics.length - 1 : audioId = 0;

        playAudio(audioId);
    }else{
        if (localStorage.getItem("gen_loading-url")) {
            if (player2.getPlayerState() == 1) {
                player2.pauseVideo();
                paused = true;
            }
        }
        playAudio(0);
    }
}

function isValidYouTubeUrl(url) {
    var pattern = /^(https?:\/\/)?(www\.)?(youtube\.com\/(?:[^\/\n\s]+\/\S+\/|(?:v|e(?:mbed)?)\/|\S*?[?&]v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})/;
    return pattern.test(url);
}