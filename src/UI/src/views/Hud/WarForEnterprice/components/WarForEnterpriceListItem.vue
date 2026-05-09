<template>
  <div v-if="item.timePassed < item.time" class="war-for-enterprice-list-item">
    <div class="war-for-enterprice-list-item__info">
      <span class="name-family">{{ currentOrgName }}</span>
      <span>{{ loc("war_for_enterprice_6") }}</span>
      <span class="name-company">{{ companiesConfig[item.key].Name }}</span>
    </div>

    <div class="war-for-enterprice-list-item__line"></div>

    <div class="war-for-enterprice-list-item__circle">
      <div class="war-for-enterprice-list-item__circle-img">
        <svg viewBox="0 0 42 42" xmlns="http://www.w3.org/2000/svg">
          <circle
            cx="21"
            cy="21"
            r="19"
            :stroke="'rgba(255, 255, 255, 0.07)'"
            stroke-width="4"
            fill="none"
            stroke-dasharray="120"
            stroke-dashoffset="0"
          />
          <circle
            cx="21"
            cy="21"
            r="19"
            stroke="#301934 "
            stroke-width="4"
            fill="none"
            :stroke-dasharray="calcCircumference"
            :stroke-dashoffset="calcCircumference - -circleValue"
          />
        </svg>
        <img src="/img/hud/artimer/time.svg" alt="" />
      </div>

      <div class="war-for-enterprice-list-item__circle-time">
        <span>{{ formatTimeLeft }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
import warcompanies from "../../../../configs/families/warcompanies";
export default {
  name: "WarForEnterpriceListItem",
  props: {
    item: Object,
  },
  computed: {
    ...mapState("familyMenu/ratingPage", ["orgList"]),
    ...mapState("warForEnterprice", ["fractionNames"]),
    ...mapGetters("localization", ["loc"]),
    companiesConfig() {
      return warcompanies;
    },
    currentOrgName() {
      if (this.item.orgId > 0) return this.item.orgName;
      return "Unknown";
    },
    calcCircumference: function () {
      let number = 2 * Math.PI * 19;/* 22 */
      return number;
    },
    circleValue: function () {
      const maxCircleValue = this.calcCircumference;
      const maxCircleValuePer =
        (maxCircleValue / this.item.time) * this.item.timePassed;

      return maxCircleValue - maxCircleValuePer;
    },
    formatTimeLeft: function () {
      let minutes = Math.floor((this.item.time - this.item.timePassed) / 60);
      let seconds = (this.item.time - this.item.timePassed) % 60;

      if (seconds < 10) {
        seconds = `0${seconds}`;
      }
      if (minutes < 10) {
        minutes = `0${minutes}`;
      }

      return `${minutes}:${seconds}`;
    },
  },
};
</script>

<style lang="scss" scoped>
$width: 1920;
$height: 1080;

@function conv($px, $direction: false) {
  @if $direction {
    @return ($px / $height) * 100vh;
  } @else {
    @return ($px / $width) * 100vw;
  }
}

.war-for-enterprice-list-item {
  display: flex;
  align-items: center;
  z-index: 1;
  font-family: "Akrobat";

  span,
  button {
    font-family: "Akrobat";
  }

  &__info {
    display: flex;
    flex-direction: column;
    align-items: flex-end;

    span {
      display: block;
      text-align: right;
      text-transform: uppercase;
      font-weight: 700;

      &:nth-child(2) {
        font-size: conv(16, true);
        line-height: conv(19, true);
        color: rgba(255, 255, 255, 0.55);
      }
    }
  }

  &__line {
    margin: 0 conv(15);
    width: conv(2);
    height: conv(48, true);
    background: #ffffff;
    box-shadow: 0px 0px conv(5, true) rgba(255, 255, 255, 0.5);
  }

  &__circle {
    display: flex;
    align-items: center;

    &-img {
      margin-right: conv(9);
      position: relative;
      width: conv(43, true);
      height: conv(43, true);
      background: rgba(0, 0, 0, 0.1);
      border-radius: 50%;

      svg {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%) rotate(-95deg);
        width: conv(43, true);
        height: conv(43, true);
      }

      img {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        height: conv(18, true);
        width: conv(18, true);
      }
    }

    &-time {
      font-weight: 700;
      font-size: conv(18, true);
      line-height: conv(22, true);
      text-transform: uppercase;
      color: #ffffff;
      display: inline;
      width: conv(36);
      position: relative;

      span {
        position: absolute;
        top: 50%;
        left: 0;
        transform: translateY(-50%);
      }
    }
  }
}

.name-family {
  font-size: conv(24, true);
  line-height: conv(29, true);
  color: #ffffff;
  margin-bottom: conv(3, true);
}

.name-company {
  font-size: conv(16, true);
  line-height: conv(19, true);
  color: #fdff8f;
}

/* .war-for-enterprice-list-item {
  display: flex;
  align-items: center;
  z-index: 1;
  margin-bottom: 0.9rem;
  &__circle {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 2.5rem;
    height: 2.5rem;
    border-radius: 50%;
    margin-right: 1.15rem;
    position: relative;
    &:before {
      content: "";
      width: 0.15rem;
      height: 3rem;
      position: absolute;
      right: -0.5rem;
      transform: translateX(100%);
      background: #29fff2;
    }
    &-time {
      font-weight: bold;
      font-size: 0.9rem;
      line-height: 0.9rem;
    }
    svg {
      position: absolute;
      width: 100%;
      height: 100%;
      circle {
        fill: none;
        stroke: #fff;
        cx: 25;
        cy: 25;
        r: 22;
        stroke-width: 3;
      }
    }
  }
  &__info {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    span {
      margin-bottom: 0.1rem;
      font-size: 1rem;
      line-height: 1rem;
      font-weight: 300;
      &.name {
        &-family,
        &-company {
          font-weight: bold;
        }
        &-company {
          color: #29fff2;
        }
      }
    }
  }
} */
</style>
