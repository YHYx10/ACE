<template>
  <div class="help-page">
    <div class="help-page__head">
      <div class="head-item" v-for="item in headNav" :key="item.id">
        {{ loc(item) }}
      </div>
    </div>
    <div class="help-page__body scroll">
      <div class="body-item" v-for="item in helpList" :key="item.id">
        <div>{{ isReportDate(item.time * 1000) }}</div>
        <div v-if="item.code != 999">{{ loc("Pda_1") }}-{{ item.code }}</div>
        <div v-else>call</div>
        <div>{{ item.name }}</div>
        <div>{{ item.distance }}m</div>
        <div
          class="help"
          v-if="item.helpers.length > 0"
        >
          <div v-for="(elem, idx) in item.helpers" :key="idx">
            {{ elem.name }}
          </div>
          <!-- {{ item.helpers.length }}
          <div class="icon"></div> -->
        </div>
        <div v-else>
          <div class="space"></div>
        </div>
        <div>
          <div
            :class="[
              { army: type === 2 },
              { fbi: type === 1 },
              'btn',
              'btn-help',
            ]"
            @click="toHelp(item.id)"
          >
            <span>{{ loc("Pda_3") }}</span>
          </div>
        </div>
      </div>
    </div>
    <modal-helpers
      v-if="modalShow"
      :currentHelpers="currentHelpers"
      @closeModal="closeModal"
    />
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";

/* import ModalHelpers from '../components/ModalHelpers' */

export default {
  name: "HelpPage",

  components: {
    /* ModalHelpers */
  },

  data: function () {
    return {
      headNav: ["Pda_22", "Pda_1", "Pda_14", "Pda_23", "Pda_5"],
      currentHelpers: null,
      modalShow: false,
    };
  },

  computed: {
    ...mapState("personalDigitalAssistant", ["helpList", "type"]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    toWatchHelpers: function (array) {
      this.currentHelpers = array;
      this.modalShow = true;
    },
    closeModal: function () {
      this.modalShow = false;
      this.currentHelpers = null;
    },
    toHelp: function (id) {
      window.mp.trigger("pda:toHelp", id);
    },
    isReportDate: function (value) {
      let time = new Date(value).toLocaleTimeString("ru-RU");
      let date = new Date(value).toLocaleDateString("ru-RU");
      return date + " " + time;
    },
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.help-page {
  width: 100%;
  max-height: conv(513);
  display: flex;
  flex-flow: column;

  &__head {
    padding-left: conv(20);
    display: grid;
    grid-template-columns: conv(57) conv(63) conv(198) conv(89) auto;
    column-gap: conv(20);
    width: 100%;
    margin-bottom: conv(16);

    .head-item {
      font-weight: 700;
      font-size: conv(15);
      line-height: conv(18);
      display: flex;
      align-items: center;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.5);
    }
  }

  &__body {
    width: 100%;
    display: flex;
    flex-direction: column;
    /* max-height: conv(412); */
    height: 100%;
    max-height: 100%;
    padding-right: conv(10);
    overflow-x: hidden;
    overflow-y: auto;
  }

  /* &__body .body-item > div {
    &:nth-child(1) {
      width: 7.55rem;
      min-width: 7.55rem;
    }
    &:nth-child(2) {
      width: 7.9rem;
      min-width: 7.9rem;
    }
    &:nth-child(3) {
      width: 11.95rem;
      min-width: 11.95rem;
    }
    &:nth-child(4) {
      width: 10.55rem;
      min-width: 10.55rem;
    }
    &:nth-child(5) {
      width: 4.65rem;
      min-width: 4.65rem;
      justify-content: center;
    }
  } */
  &__body {
    .body-item {
      padding-left: conv(20);
      display: grid;
      grid-template-columns: conv(57) conv(63) conv(198) conv(89) auto auto;
      column-gap: conv(20);
      width: 100%;
      height: auto;
      /* min-height: conv(80); */
      border: 1px solid rgba(255, 255, 255, 0.09);
      font-weight: 700;
      font-size: conv(18);
      line-height: conv(22);
      text-transform: uppercase;
      color: #ffffff;

      &:not(:last-child) {
        margin-bottom: conv(3);
      }

      & > div {
        min-height: conv(78);
      }

      .help {
        padding: conv(6) 0;
        display: flex;
        flex-direction: column;
        white-space: nowrap;
        align-items: flex-start;
        justify-content: center;
      }
      /* .icon {
        margin-left: 0.5rem;
        width: 1.2rem;
        height: 1.2rem;
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
        background-image: url("/img/personalDigitalAssistant/info.svg");
      } */
      .space {
        width: 1.2rem;
        height: 0.1rem;
        background: #ffffff;
      }
      & > div {
        display: flex;
        align-items: center;
        justify-content: flex-start;

        &:last-child {
          width: 100%;
          justify-content: flex-end;
        }
      }

      .btn-help {
        width: conv(139);
        min-width: conv(139);
        height: 100%;
        cursor: pointer;
        background: rgba(255, 255, 255, 0.03);
        display: flex;
        align-items: center;
        justify-content: center;
        position: relative;
        font-weight: 700;
        font-size: conv(18);
        line-height: conv(22);
        text-align: center;
        text-transform: uppercase;
        color: #ffffff;

        span {
          z-index: 3;
          position: relative;
        }

        &::after {
          content: "";
          position: absolute;
          top: 0;
          left: 0;
          width: 100%;
          height: 100%;
          background: linear-gradient(
            180deg,
            #301934  0%,
            #591b87 100%,
            #591b87 100%
          );
          opacity: 0;
          transition: 0.2s ease;
          z-index: 2;
        }

        &:hover::after {
          opacity: 1;
        }

        /* background: #ffd130;
        border-radius: 0.4rem;
        padding: 0.6rem;
        width: fit-content;
        color: #000;
        font-weight: bold;
        font-size: 1rem;
        &.fbi {
          color: #fff;
          background: #fb7712;
        }
        &.army {
          color: #fff;
          background: #6c8073;
        }
        &:hover {
          transform: scale(1.1);
        }
        &:active {
          transform: scale(1.05);
        } */
      }
    }
  }
}

/* Premium MDT dispatch/calls pass */
.help-page {
  height: 100% !important;
  max-height: none !important;
  position: relative !important;

  &::before {
    content: "DISPATCH / ACTIVE CALLS";
    display: flex;
    align-items: center;
    height: conv(34);
    margin-bottom: conv(10);
    padding-left: conv(14);
    border-left: conv(4) solid #43b7ff;
    font-weight: 900;
    font-size: conv(24);
    line-height: conv(28);
    letter-spacing: 0.075em;
    text-transform: uppercase;
    color: #fff;
    text-shadow: 0 0 conv(16) rgba(67, 183, 255, 0.3);
  }

  &::after {
    content: "LIVE CAD FEED";
    position: absolute;
    top: conv(8);
    right: conv(10);
    height: conv(24);
    padding: 0 conv(10) 0 conv(24);
    display: flex;
    align-items: center;
    border-radius: conv(7);
    border: 1px solid rgba(95, 255, 184, 0.16);
    background: rgba(95, 255, 184, 0.055);
    font-weight: 900;
    font-size: conv(9);
    line-height: conv(11);
    letter-spacing: 0.16em;
    color: rgba(95, 255, 184, 0.85);
    box-shadow: 0 0 conv(14) rgba(95, 255, 184, 0.08);
  }

  &__head {
    padding: 0 conv(14) !important;
    margin-bottom: conv(7) !important;
    color: rgba(155, 212, 255, 0.58) !important;

    .head-item {
      font-size: conv(12) !important;
      line-height: conv(15) !important;
      letter-spacing: 0.08em !important;
      color: rgba(155, 212, 255, 0.58) !important;
    }
  }

  &__body {
    height: calc(100% - 4.4rem) !important;
    padding-right: conv(8) !important;
  }

  &__body .body-item {
    position: relative !important;
    min-height: conv(68) !important;
    padding-left: conv(16) !important;
    margin-bottom: conv(8) !important;
    border-radius: conv(12) !important;
    border-color: rgba(104, 191, 255, 0.15) !important;
    background:
      radial-gradient(circle at 0 50%, rgba(31, 139, 255, 0.12), transparent 34%),
      linear-gradient(135deg, rgba(255, 255, 255, 0.05), rgba(255, 255, 255, 0.017)),
      rgba(3, 12, 25, 0.66) !important;
    box-shadow: inset 0 0 0 1px rgba(255, 255, 255, 0.018), 0 0 conv(18) rgba(0, 0, 0, 0.15) !important;
    overflow: hidden !important;

    &::before {
      content: "";
      position: absolute;
      left: 0;
      top: 12%;
      width: conv(4);
      height: 76%;
      border-radius: 999px;
      background: #43b7ff;
      box-shadow: 0 0 conv(16) rgba(67, 183, 255, 0.62);
    }

    &::after {
      content: "RESPONDING UNITS";
      position: absolute;
      top: conv(7);
      right: conv(132);
      font-weight: 900;
      font-size: conv(8);
      line-height: conv(10);
      letter-spacing: 0.16em;
      color: rgba(116, 202, 255, 0.64);
    }

    & > div {
      min-height: conv(68) !important;
      font-size: conv(15) !important;
      line-height: conv(18) !important;
      letter-spacing: 0.025em !important;
    }

    .help {
      color: rgba(190, 229, 255, 0.76) !important;
      font-size: conv(12) !important;
      line-height: conv(15) !important;
    }

    .space {
      width: conv(28) !important;
      height: conv(3) !important;
      border-radius: 999px !important;
      background: rgba(255, 86, 86, 0.58) !important;
      box-shadow: 0 0 conv(10) rgba(255, 86, 86, 0.28) !important;
    }

    .btn-help {
      width: conv(104) !important;
      min-width: conv(104) !important;
      height: conv(40) !important;
      margin-right: conv(10) !important;
      border-radius: conv(9) !important;
      border: 1px solid rgba(95, 255, 184, 0.24) !important;
      background: rgba(95, 255, 184, 0.1) !important;
      color: #a7ffd0 !important;
      font-size: conv(12) !important;
      letter-spacing: 0.07em !important;
      box-shadow: 0 0 conv(16) rgba(95, 255, 184, 0.09) !important;

      &::after {
        background: linear-gradient(135deg, rgba(95, 255, 184, 0.38), rgba(31, 139, 255, 0.22)) !important;
      }

      &:hover {
        color: #fff !important;
        transform: translateY(-1px) !important;
      }
    }
  }
}

/* Asset-backed MDT calls styling */
.help-page {
  &::before {
    padding-left: conv(52) !important;
    min-height: conv(44) !important;
    background: url("/img/mdt/calls.svg") conv(12) center / conv(30) conv(30) no-repeat !important;
  }

  &__body .body-item {
    background:
      radial-gradient(circle at 0 50%, rgba(57, 188, 255, 0.2), transparent 30%),
      linear-gradient(135deg, rgba(255, 255, 255, 0.058), rgba(255, 255, 255, 0.016)),
      rgba(2, 11, 25, 0.78) !important;
    border-color: rgba(98, 202, 255, 0.22) !important;
    min-height: conv(74) !important;

    &:hover {
      transform: translateX(conv(2));
      border-color: rgba(108, 217, 255, 0.34) !important;
      box-shadow: inset 0 0 conv(28) rgba(42, 160, 255, 0.08), 0 0 conv(22) rgba(42, 160, 255, 0.12) !important;
    }
  }
}

/* PDF-layout correction pass: dispatch table */
.help-page {
  &::before {
    justify-content: center !important;
    height: conv(62) !important;
    min-height: conv(62) !important;
    margin-bottom: conv(18) !important;
    padding-left: 0 !important;
    background-position: calc(50% - #{conv(205)}) center !important;
    font-size: conv(34) !important;
    line-height: conv(38) !important;
    letter-spacing: 0.1em !important;
    border-left: 0 !important;
  }

  &::after {
    top: conv(15) !important;
    right: conv(18) !important;
  }

  &__head {
    height: conv(44) !important;
    padding: 0 conv(18) !important;
    align-items: center !important;
    border: 1px solid rgba(117, 204, 255, 0.16) !important;
    background: rgba(0, 0, 0, 0.34) !important;
    margin-bottom: conv(8) !important;
  }

  &__body {
    height: calc(100% - #{conv(118)}) !important;
  }

  &__body .body-item {
    min-height: conv(78) !important;
    border-radius: conv(3) !important;
    border-color: rgba(117, 204, 255, 0.18) !important;
    background: linear-gradient(90deg, rgba(9, 22, 50, 0.76), rgba(16, 35, 78, 0.5), rgba(5, 12, 28, 0.7)) !important;
  }
}
</style>