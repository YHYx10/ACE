<template>
  <div class="notification" :class="typeClass" :style="accentStyle">
    <div class="notification__corner notification__corner--tl"></div>
    <div class="notification__corner notification__corner--tr"></div>
    <div class="notification__corner notification__corner--bl"></div>
    <div class="notification__corner notification__corner--br"></div>
    <img class="notification__glow" src="/img/notifications/corners-glow.png" alt="">

    <div class="notification__icon">
      <img :src="iconPath" alt="">
    </div>

    <div class="notification__content">
      <div class="notification__title">{{ item.type.title }}</div>
      <div class="notification__text">{{ message }}</div>
    </div>

    <div class="notification__progress"></div>
  </div>
</template>

<script>
import { mapMutations, mapGetters } from "vuex";

let lastSoundAt = 0;

export default {
  name: "Notification",
  props: {
    item: Object,
  },
  data: function () {
    return {
      message: "",
      timer: null,
    };
  },
  computed: {
    ...mapGetters("localization", ["loc"]),
    accentStyle: function () {
      return {
        "--notify-accent": this.item.type.color,
        "--notify-tint": this.hexToRgba(this.item.type.color, 0.12),
        "--notify-tint-soft": this.hexToRgba(this.item.type.color, 0.055),
        "--notify-duration": `${Math.max(Number(this.item.time) || 3000, 300)}ms`,
      };
    },
    iconPath: function () {
      return `/img/notifications/${this.item.type.icon || "info.png"}`;
    },
    typeClass: function () {
      const title = `${this.item.type.title || ""}`.toLowerCase();
      if (title.indexOf("success") !== -1) return "notification--success";
      if (title.indexOf("error") !== -1) return "notification--error";
      if (title.indexOf("warning") !== -1) return "notification--warning";
      return "notification--info";
    },
  },
  methods: {
    ...mapMutations("notifyList", ["removeItem"]),
    buildMessage: function () {
      this.message = this.loc(this.item.message);
    },
    hexToRgba: function (hex, alpha) {
      const clean = `${hex || "#fff000"}`.replace("#", "");
      const normalized = clean.length === 3
        ? clean.split("").map((char) => char + char).join("")
        : clean;
      const value = parseInt(normalized, 16);
      if (Number.isNaN(value)) return `rgba(255, 240, 0, ${alpha})`;
      const r = (value >> 16) & 255;
      const g = (value >> 8) & 255;
      const b = value & 255;
      return `rgba(${r}, ${g}, ${b}, ${alpha})`;
    },
    playSound: function () {
      try {
        const now = Date.now();
        if (now - lastSoundAt < 140) return true;
        lastSoundAt = now;

        const audio = new Audio("/sounds/notifications/notification.mp3");
        audio.volume = 0.075;
        const playPromise = audio.play();
        if (playPromise && typeof playPromise.catch === "function") {
          playPromise.catch(() => {});
        }
      } catch (e) {
        return false;
      }
      return true;
    },
  },
  mounted: function () {
    this.buildMessage();
    this.playSound();
    this.timer = setTimeout(() => {
      this.removeItem(this.item);
    }, this.item.time);
  },
  beforeDestroy: function () {
    if (this.timer) clearTimeout(this.timer);
  },
};
</script>

<style lang="scss" scoped>
.notification {
  --notify-accent: #fff000;
  width: min(25.25rem, 100%);
  min-height: 4.95rem;
  position: relative;
  display: flex;
  align-items: center;
  gap: .95rem;
  padding: .86rem 1.08rem .88rem .92rem;
  border: 1px solid rgba(255, 255, 255, .12);
  border-radius: .96rem;
  background:
    radial-gradient(circle at 8% 14%, rgba(255, 255, 255, .105), transparent 34%),
    radial-gradient(circle at 100% 10%, var(--notify-tint), transparent 36%),
    radial-gradient(circle at 88% 92%, var(--notify-tint-soft), transparent 42%),
    linear-gradient(135deg, rgba(30, 33, 38, .72), rgba(8, 10, 15, .66));
  backdrop-filter: blur(.72rem) saturate(1.18);
  box-shadow:
    0 1.25rem 2.1rem rgba(0, 0, 0, .5),
    0 .28rem .95rem rgba(0, 0, 0, .32),
    0 0 1.35rem var(--notify-tint),
    inset 0 1px 0 rgba(255, 255, 255, .09),
    inset 0 0 1.55rem rgba(255, 255, 255, .035);
  overflow: hidden;
  pointer-events: auto;
  transform-origin: right center;
  transition: transform .22s ease, filter .22s ease, box-shadow .22s ease, background .22s ease;
  animation: notificationDepth 1.7s ease-in-out infinite alternate;

  &:hover {
    filter: brightness(1.09);
    transform: scale(1.02);
    box-shadow:
      0 1.35rem 2.3rem rgba(0, 0, 0, .52),
      0 .35rem 1.1rem rgba(0, 0, 0, .34),
      0 0 1.55rem var(--notify-tint),
      0 0 .76rem var(--notify-accent),
      inset 0 1px 0 rgba(255, 255, 255, .105),
      inset 0 0 1.65rem rgba(255, 255, 255, .045);
  }

  &:hover &__progress {
    animation-play-state: paused;
  }

  &__glow {
    position: absolute;
    inset: -.44rem -.42rem auto auto;
    width: 8.3rem;
    height: auto;
    opacity: .24;
    mix-blend-mode: screen;
    pointer-events: none;
    filter: drop-shadow(0 0 .54rem var(--notify-accent));
  }

  &__icon {
    width: 2.86rem;
    height: 2.86rem;
    flex: 0 0 2.86rem;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: .82rem;
    background: rgba(255, 255, 255, .055);
    box-shadow:
      inset 0 0 .85rem rgba(255, 255, 255, .035),
      0 0 .85rem var(--notify-tint);
    animation-duration: .64s;
    animation-fill-mode: both;
    animation-timing-function: cubic-bezier(.16, 1, .3, 1);

    img {
      width: 1.84rem;
      height: 1.84rem;
      object-fit: contain;
      filter: drop-shadow(0 0 .3rem var(--notify-accent));
    }
  }

  &--success &__icon {
    animation-name: iconSuccessPop;
  }

  &--error &__icon {
    animation-name: iconErrorShake;
    animation-duration: .42s;
  }

  &--warning &__icon {
    animation-name: iconWarningPulse;
    animation-duration: 1.15s;
    animation-iteration-count: 2;
  }

  &--info &__icon {
    animation-name: iconInfoFade;
  }

  &__content {
    min-width: 0;
    flex: 1;
    font-family: 'Akrobat';
  }

  &__title {
    margin-bottom: .42rem;
    color: var(--notify-accent);
    font-size: .94rem;
    line-height: 1;
    font-weight: 900;
    letter-spacing: .18rem;
    text-shadow: 0 0 .48rem var(--notify-accent);
  }

  &__text {
    color: rgba(255, 255, 255, .94);
    font-size: .84rem;
    line-height: 1.26;
    letter-spacing: .035rem;
    white-space: pre-wrap;
    overflow-wrap: anywhere;
    text-shadow: 0 .08rem .22rem rgba(0, 0, 0, .62);
  }

  &__corner {
    position: absolute;
    width: 1.55rem;
    height: 1.38rem;
    border-color: var(--notify-accent);
    filter: drop-shadow(0 0 .22rem var(--notify-accent));
    opacity: .88;
    pointer-events: none;
    animation: cornerGlow .95s ease-in-out infinite alternate;

    &--tl {
      left: -1px;
      top: -1px;
      border-top: .12rem solid;
      border-left: .12rem solid;
      border-top-left-radius: .96rem;
    }

    &--tr {
      right: -1px;
      top: -1px;
      border-top: .12rem solid;
      border-right: .12rem solid;
      border-top-right-radius: .96rem;
    }

    &--bl {
      left: -1px;
      bottom: -1px;
      border-bottom: .12rem solid;
      border-left: .12rem solid;
      border-bottom-left-radius: .96rem;
    }

    &--br {
      right: -1px;
      bottom: -1px;
      border-bottom: .12rem solid;
      border-right: .12rem solid;
      border-bottom-right-radius: .96rem;
    }
  }

  &__progress {
    position: absolute;
    left: 0;
    right: 0;
    bottom: 0;
    height: .16rem;
    background: linear-gradient(90deg, var(--notify-accent), rgba(255, 255, 255, .82));
    box-shadow:
      0 0 .42rem var(--notify-accent),
      0 -.12rem .5rem var(--notify-tint);
    transform-origin: left center;
    animation: progressDrain var(--notify-duration) linear forwards;
  }
}

@keyframes cornerGlow {
  from {
    opacity: .36;
  }
  to {
    opacity: .86;
  }
}

@keyframes notificationDepth {
  from {
    box-shadow:
      0 1.08rem 1.9rem rgba(0, 0, 0, .5),
      0 .24rem .85rem rgba(0, 0, 0, .32),
      0 0 .9rem var(--notify-tint),
      inset 0 1px 0 rgba(255, 255, 255, .08),
      inset 0 0 1.4rem rgba(255, 255, 255, .032);
  }
  to {
    box-shadow:
      0 1.16rem 2.02rem rgba(0, 0, 0, .52),
      0 .3rem .98rem rgba(0, 0, 0, .34),
      0 0 1.28rem var(--notify-tint),
      inset 0 1px 0 rgba(255, 255, 255, .095),
      inset 0 0 1.5rem rgba(255, 255, 255, .04);
  }
}

@keyframes progressDrain {
  from {
    transform: scaleX(1);
  }
  to {
    transform: scaleX(0);
  }
}

@keyframes iconSuccessPop {
  0% {
    opacity: .35;
    transform: scale(.72);
  }
  58% {
    transform: scale(1.12);
  }
  100% {
    opacity: 1;
    transform: scale(1);
  }
}

@keyframes iconErrorShake {
  0%, 100% {
    transform: translateX(0);
  }
  22% {
    transform: translateX(-.1rem);
  }
  48% {
    transform: translateX(.12rem);
  }
  72% {
    transform: translateX(-.06rem);
  }
}

@keyframes iconWarningPulse {
  0%, 100% {
    transform: scale(1);
  }
  50% {
    transform: scale(1.08);
    box-shadow:
      inset 0 0 .9rem rgba(255, 255, 255, .045),
      0 0 1rem var(--notify-tint),
      0 0 .45rem var(--notify-accent);
  }
}

@keyframes iconInfoFade {
  0% {
    opacity: 0;
    transform: translateY(.16rem);
  }
  100% {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>
