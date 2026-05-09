<template>
  <div class="interaction-menu">
    <div class="interaction-menu__items">
      <div
        v-for="(item, index) in items"
        :key="index"
        class="item"
        @click="someEvent(item.key)"
        :class="'a' + index"
      >
        <div class="item__main">
          <svg
            width="144"
            height="186"
            viewBox="0 0 144 186"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M62.7909 3.16263C68.2076 -1.05032 75.7924 -1.05031 81.2091 3.16264L138.209 47.496C141.863 50.3378 144 54.7074 144 59.3363V126.664C144 131.293 141.863 135.662 138.209 138.504L81.2091 182.837C75.7924 187.05 68.2076 187.05 62.7909 182.837L5.79088 138.504C2.13709 135.662 0 131.293 0 126.664V59.3363C0 54.7074 2.13709 50.3378 5.79089 47.496L62.7909 3.16263Z"
              :fill="`url('#paint0_linear_130_45${index}')`"
            />
            <defs>
              <linearGradient
                :id="`paint0_linear_130_45${index}`"
                x1="35.2552"
                y1="160.801"
                x2="113.819"
                y2="28.4328"
                gradientUnits="userSpaceOnUse"
              >
                <stop stop-opacity="0.6" stop-color="black" />
                <stop offset="1" stop-opacity="0.6" stop-color="black" />
              </linearGradient>
            </defs>
          </svg>

          
          <img :src="`img/interactionMenu/icons/${item.key}.png`" :alt="item.key" :onerror="`this.onerror=null;this.src='img/interactionMenu/icons/${item.key}.svg'`" class="icon">
          <span class="text">{{ loc(item.title) }}</span>
          <!-- <span class="text">{{ loc(item.title) }}</span> -->
        </div>
      </div>
      <div class="item item-empty">
        <svg
          width="144"
          height="186"
          viewBox="0 0 144 186"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M62.7909 3.16263C68.2076 -1.05032 75.7924 -1.05031 81.2091 3.16264L138.209 47.496C141.863 50.3378 144 54.7074 144 59.3363V126.664C144 131.293 141.863 135.662 138.209 138.504L81.2091 182.837C75.7924 187.05 68.2076 187.05 62.7909 182.837L5.79088 138.504C2.13709 135.662 0 131.293 0 126.664V59.3363C0 54.7074 2.13709 50.3378 5.79089 47.496L62.7909 3.16263Z"
            fill="url('#paint0_linear_130_4555')"
          />
          <defs>
            <linearGradient
              id="paint0_linear_130_4555"
              x1="35.2552"
              y1="160.801"
              x2="113.819"
              y2="28.4328"
              gradientUnits="userSpaceOnUse"
            >
              <stop stop-opacity="0.6" stop-color="black" />
              <stop offset="1" stop-opacity="0.6" stop-color="black" />
            </linearGradient>
          </defs>
        </svg>
        <svg
          @click="close"
          width="66"
          height="66"
          viewBox="0 0 66 66"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M48.3725 13.75L33 29.1225L17.6275 13.75L13.75 17.6275L29.1225 33L13.75 48.3725L17.6275 52.25L33 36.8775L48.3725 52.25L52.25 48.3725L36.8775 33L52.25 17.6275L48.3725 13.75Z"
            fill="white"
          />
        </svg>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";
import { mapGetters } from "vuex";

export default {
  name: "InteractionMenu",

  computed: {
    ...mapState("interactionMenu", ["items"]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    someEvent: function (key) {
      window.mp.trigger("intMenu:selected", key);
    },
    close() {
      window.mp.trigger("intMenu:close");
    },
  },
};
</script>

<style lang="scss" scoped>
.interaction-menu {
  width: 100%;
  height: 100%;
  position: relative;

  &,
  div,
  span,
  button {
    font-family: "Akrobat";
  }

  &__items {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
  }

  .item {
    width: 7.579rem;
    height: 10.211rem;
    position: absolute;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    cursor: pointer;

    .item__main > svg:first-child {
      stop {
        transition: 0.3s ease;
      }
    }

    &:hover .item__main > svg:first-child {
      stop:first-child {
        stop-color: rgb(255, 255, 255) !important;
        stop-opacity: 0.8;
      }

      stop:last-child {
        stop-color: rgb(255, 255, 255) !important;
        stop-opacity: 0.8;
      }
    }

    svg {
      position: absolute;
      top: 50%;
      left: 50%;
      width: 100%;
      height: 100%;
      transform: translate(-50%, -50%);
      z-index: 1;
    }

    &__main {
      display: flex;
      align-items: center;
      flex-direction: column;
      // div,
      span {
        z-index: 2;
        position: absolute;
        top: 7.2rem;
      }

      div {
        height: 3.053rem;
        width: 100%;
      }

      img.icon {
        width: auto;
        max-width: 65%;
        max-height: 52%;
        z-index: 2;
        position: absolute;
        color: white;
        top: 45%;
        left: 50%;
        transform: translate(-50%, -50%);
      }

      span {
        margin-top: 0.105rem;
        font-weight: 900;
        font-size: 0.579rem;
        line-height: 96.5%;
        text-align: center;
        color: #ffffff;
        max-width: 6rem;
        white-space: pre-wrap;
      }
    }

    &-empty {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      cursor: pointer;

      & > svg:last-child {
        width: 3.474rem;
        height: 3.474rem;
      }

      & > svg:first-child {
        stop {
          transition: 0.3s ease;
        }
      }

      &:hover {
        & > svg:first-child {
          stop:first-child {
            stop-color: #ffffff;
            stop-opacity: 1;
          }

          stop:last-child {
            stop-color: #ffffff;
            stop-opacity: 1;
          }
        }
      }
    }

    &.a0 {
      transform: translate(calc(-100% - 0.105rem), calc(-50% - 7.421rem));
    }

    &.a1 {
      transform: translate(2px, calc(-50% - 7.421rem));
    }

    &.a2 {
      transform: translate(calc(50% + 0.21rem), -50%);
    }

    &.a3 {
      transform: translate(0.105rem, calc(-50% + 7.421rem));
    }

    &.a4 {
      transform: translate(calc(-100% - 0.105rem), calc(-50% + 7.421rem));
    }

    &.a5 {
      transform: translate(calc(-150% - 0.21rem), -50%);
    }

    &.a6 {
      transform: translate(calc(-150% - 0.21rem), calc(-50% - 14.842rem));
    }

    &.a7 {
      transform: translate(calc(-50%), calc(-50% - 14.842rem));
    }

    &.a8 {
      transform: translate(calc(50% + 0.21rem), calc(-50% - 14.842rem));
    }

    &.a9 {
      transform: translate(calc(100% + 0.315rem), calc(-50% - 7.421rem));
    }

    &.a10 {
      transform: translate(calc(150% + 0.42rem), -50%);
    }

    &.a11 {
      transform: translate(calc(100% + 0.315rem), calc(-50% + 7.421rem));
    }

    &.a12 {
      transform: translate(
        calc(50% + 0.21rem),
        calc(-50% + 14.842rem)
      ); /* calc(73% + 45px)  */
    }

    &.a13 {
      transform: translate(calc(-50%), calc(-50% + 14.842rem));
    }

    &.a14 {
      transform: translate(calc(-150% - 0.21rem), calc(-50% + 14.842rem));
    }

    &.a15 {
      transform: translate(calc(-200% - 0.315rem), calc(-50% + 7.421rem));
    }

    &.a16 {
      transform: translate(calc(-250% - 0.42rem), -50%);
    }

    &.a17 {
      transform: translate(calc(-200% - 0.315rem), calc(-50% - 7.421rem));
    }
  }
}
</style>
