<template>
  <div class="hole-information">
    <div class="hole-information_wrap">
      <div
        class="hole-information_title"
        v-if="holeInformation.itemInfo.name !== null"
      >
        <img
          :src="`/img/inventory/items/${holeInformation.itemInfo.icon}.png`"
          alt=""
        />
        {{ loc(holeInformation.itemInfo.name) }}
      </div>

      <div class="hole-information_list">
        <div
          class="hole-information__list-item"
          v-if="holeInformation.itemInfo.isWatered !== null"
        >
          <span>
            {{ loc("Hole_information_1") }}
          </span>
          <span>
            {{
              holeInformation.itemInfo.isWatered
                ? loc("Hole_information_6")
                : loc("Hole_information_7")
            }}
          </span>
        </div>
        <div
          class="hole-information__list-item"
          v-if="holeInformation.itemInfo.fertilizer !== null"
        >
          <span>
            {{ loc("Hole_information_2") }}
          </span>
          <span>
            {{ loc(holeInformation.itemInfo.fertilizer) }}
          </span>
        </div>
        <div
          class="hole-information__list-item"
          v-if="holeInformation.itemInfo.hole !== null"
        >
          <span>
            {{ loc("Hole_information_3") }}
          </span>
          <span>
            {{ loc(holeInformation.itemInfo.hole) }}
          </span>
        </div>
      </div>
    </div>
    <div v-if="holeInformation.stateInfo.value > 0" class="hole-information_timer">
      <img src="/img/hud/artimer/time.svg" alt="" />
      <div>
        <span v-if="holeInformation.stateInfo.value > 0"
          >{{
            holeInformation.stateInfo.isWithers
              ? loc("Hole_information_8")
              : loc("Hole_information_9")
          }}
          {{ loc("Hole_information_4") }}
        </span>
        <span
          >{{ holeInformation.stateInfo.value }}
          {{ loc("Hole_information_5") }}</span
        >
      </div>
    </div>

    <!-- <div
      class="hole-information__title"
      v-if="holeInformation.itemInfo.name !== null"
    >
      <div
        class="hole-information__title-icon"
        :style="{
          backgroundImage: `url(/img/inventory/items/${holeInformation.itemInfo.icon}.png)`,
        }"
      ></div>
      <div class="hole-information__title__info">
        <div
          class="hole-information__title__info-descmy"
          v-if="holeInformation.itemInfo.isMy"
        >
          {{ loc("Hole_information_0") }}
        </div>
        <div class="hole-information__title__info-desc" v-else>
          {{ loc("Hole_information_12") }}
        </div>
        <div class="hole-information__title__info-name">
          {{ loc(holeInformation.itemInfo.name) }}
        </div>
      </div>
    </div>
    <div class="hole-information__list">
      <div
        class="hole-information__list-item is-watered"
        v-if="holeInformation.itemInfo.isWatered !== null"
      >
        <div class="hole-information__list-item-desc">
          {{ loc("Hole_information_1") }}
        </div>
        <div class="hole-information__list-item-value">
          {{
            holeInformation.itemInfo.isWatered
              ? loc("Hole_information_6")
              : loc("Hole_information_7")
          }}
        </div>
      </div>
      <div
        class="hole-information__list-item fertilizer"
        v-if="holeInformation.itemInfo.fertilizer !== null"
      >
        <div class="hole-information__list-item-desc">
          {{ loc("Hole_information_2") }}
        </div>
        <div class="hole-information__list-item-value">
          {{ loc(holeInformation.itemInfo.fertilizer) }}
        </div>
      </div>
      <div
        class="hole-information__list-item hole"
        v-if="holeInformation.itemInfo.hole !== null"
      >
        <div class="hole-information__list-item-desc">
          {{ loc("Hole_information_3") }}
        </div>
        <div class="hole-information__list-item-value">
          {{ loc(holeInformation.itemInfo.hole) }}
        </div>
      </div>
    </div>
    <div
      :class="[
        { withers: holeInformation.stateInfo.isWithers },
        'hole-information__state',
      ]"
      v-if="holeInformation.stateInfo.show"
    >
      <div
        class="hole-information__state-desc"
        v-if="holeInformation.stateInfo.value > 0"
      >
        {{
          holeInformation.stateInfo.isWithers
            ? loc("Hole_information_8")
            : loc("Hole_information_9")
        }}
        {{ loc("Hole_information_4") }}
        <span
          >{{ holeInformation.stateInfo.value }}
          {{ loc("Hole_information_5") }}</span
        >
      </div>
      <div class="hole-information__state-value" v-else>
        {{
          holeInformation.stateInfo.isWithers
            ? loc("Hole_information_10")
            : loc("Hole_information_11")
        }}
      </div>
      <div
        class="hole-information__state-progress"
        :style="{ width: `${progressWidth}%` }"
      ></div>
    </div> -->
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
export default {
  name: "HoleInformation",
  computed: {
    ...mapGetters("localization", ["loc"]),
    ...mapState("hud", ["holeInformation"]),
    progressWidth() {
      return (
        (this.holeInformation.stateInfo.value /
          this.holeInformation.stateInfo.maxValue) *
        100
      );
    },
  },
};
</script>

<style lang="scss">
.hole-information {
  position: absolute;
  bottom: 15.474rem;
  right: 1.579rem;
  animation: holeShow 0.7s;
  width: 13.105rem;

  & > div {
    width: 100%;
  }

  &_wrap {
    background: rgba(12, 14, 20, 0.5);
    padding: 0.895rem 0 0.947rem 1.421rem;
  }

  &_title {
    display: flex;
    align-items: center;
    font-family: "Akrobat";
    font-style: normal;
    font-weight: 700;
    font-size: 1.263rem;
    line-height: 1.526rem;
    text-transform: uppercase;
    color: #ffffff;
    margin-bottom: 0.737rem;

    img {
      width: 1.579rem;
      height: 1.579rem;
      margin-right: 0.947rem;
    }
  }

  &_list {
    div {
      display: flex;
      flex-direction: column;

      &:not(:last-child) {
        margin-bottom: 0.316rem;
      }

      span {
        font-family: "Akrobat";
        font-weight: 700;
        text-transform: uppercase;

        &:first-child {
          font-size: 0.842rem;
          line-height: 1rem;
          color: rgba(255, 255, 255, 0.55);
        }

        &:last-child {
          font-size: 1.263rem;
          line-height: 1.526rem;
          color: #ffffff;
        }
      }
    }
  }

  &_timer {
    background: rgba(12, 14, 20, 0.8);
    display: flex;
    align-items: center;
    padding: 0.737rem 0 0.684rem 1.316rem;
    margin-top: 0.158rem;

    img {
      width: 1.579rem;
      height: 1.579rem;
      margin-right: 1.105rem;
    }

    div {
      span {
        display: block;
        font-family: "Akrobat";
        font-weight: 700;
        text-transform: uppercase;

        &:first-child {
          font-size: 0.842rem;
          line-height: 1rem;
          color: rgba(255, 255, 255, 0.55);
        }

        &:last-child {
          font-size: 1.263rem;
          line-height: 1.526rem;
          color: #ffffff;
        }
      }
    }
  }

  /* display: flex;
  flex-direction: column;
  position: absolute;
  right: 1.5rem;
  bottom: 0;
  width: 15rem;
  height: 15.75rem;
  background: radial-gradient(
      73.17% 64.56% at 50% 0%,
      rgba(41, 255, 242, 0.4) 0%,
      rgba(41, 255, 242, 0) 100%
    ),
    linear-gradient(
      180deg,
      rgba(13, 39, 38, 0.8) 64.56%,
      rgba(27, 63, 61, 0) 120.88%
    );
  padding: 5.5rem 1.5rem 0 1.5rem;
  text-transform: uppercase;
  color: #ffffff;
  animation: holeShow 0.7s;
  &__title {
    display: flex;
    align-items: center;
    position: absolute;
    left: 1.5rem;
    top: 1.5rem;
    &-icon {
      width: 3rem;
      height: 3rem;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      margin-right: 0.6rem;
    }
    &__info {
      display: flex;
      flex-direction: column;
      &-descmy {
        font-size: 1rem;
        line-height: 1.2rem;
        color: #b6d300;
        text-shadow: 0 0 1.5rem #b6d300;
      }
      &-desc {
        font-size: 1rem;
        line-height: 1.2rem;
        color: #d33100;
        text-shadow: 0 0 1.5rem #d33100;
      }
      &-name {
        font-weight: bold;
        font-size: 1.5rem;
        line-height: 1.5rem;
      }
    }
  }
  &__list {
    display: flex;
    flex-direction: column;
    &-item {
      display: flex;
      align-items: flex-start;
      margin-left: 1.5rem;
      border-bottom: 1px solid rgba(255, 255, 255, 0.2);
      padding-bottom: 0.25rem;
      font-size: 1rem;
      line-height: 1rem;
      margin-bottom: 0.8rem;
      position: relative;
      &:before {
        content: "";
        width: 1rem;
        height: 1rem;
        margin-left: -0.5rem;
        margin-top: 0.1rem;
        transform: translateX(-100%);
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
        box-sizing: border-box;
      }
      &-desc {
        color: rgba(255, 255, 255, 0.7);
        margin-right: 0.4rem;
      }
      &-value {
        font-weight: bold;
      }
      &.is-watered {
        &:before {
          background-image: url("/img/holeInformation/drop.png");
        }
      }
      &.fertilizer {
        &:before {
          background-image: url("/img/holeInformation/shopping-basket.png");
        }
      }
      &.hole {
        &:before {
          border-radius: 50%;
          border: 0.355rem solid #fff;
        }
      }
    }
  }
  &__state {
    width: 100%;
    height: 2rem;
    display: flex;
    align-items: center;
    justify-content: flex-start;
    position: absolute;
    left: 0;
    bottom: 0;
    background-color: rgba(13, 39, 38, 0.8);
    color: #29fff2;
    padding: 0 1.5rem;
    font-size: 1rem;
    line-height: 1.2rem;
    &.withers {
      background-color: rgba(58, 24, 21, 0.8);
      color: #ee443a;
      .hole-information__state-value {
        text-shadow: 0.5rem 0 2rem #ee443a;
      }
      .hole-information__state-progress {
        background-color: #ee443a;
      }
    }
    &-desc {
      color: rgba(255, 255, 255, 0.7);
      white-space: pre;
      span {
        color: #fff;
      }
    }
    &-value {
      width: 100%;
      text-align: center;
      font-weight: bold;
    }
    &-progress {
      background-color: #29fff2;
      height: 0.3rem;
      position: absolute;
      left: 0;
      bottom: 0;
    }
  } */
}
@keyframes holeShow {
  from {
    transform: translateY(100%);
  }
  to {
    transform: translateY(0);
  }
}
</style>
