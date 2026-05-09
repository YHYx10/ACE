<template>
  <div class="personal-digital-assistant">
    <div class="personal-digital-assistant__display">
      <div class="personal-digital_header">
        <div :class="[{ army: type === 2 }, { fbi: type === 1 }, 'head']">
          <div
            class="icon"
            :style="{
              backgroundImage:
                'url(/img/personalDigitalAssistant/logo-' + type + '.png)',
            }"
          ></div>
          <div class="head__info" v-if="type === 0">
            <div class="title">LOS SANTOS<br />POLICE DEPARTMENT</div>
            <div class="terminal-subtitle">MOBILE DATA TERMINAL</div>
          </div>
          <div class="head__info" v-else-if="type === 1">
            <div class="title">federal investigation<br />bereau</div>
            <div class="terminal-subtitle">FEDERAL CASE NETWORK</div>
          </div>
          <div class="head__info" v-else>
            <div class="title">Los santos<br />army</div>
            <div class="terminal-subtitle">TACTICAL COMMAND LINK</div>
          </div>
        </div>
        <div class="terminal-status">
          <div class="terminal-status__grid">
            <div class="status-chip officer">
              <span class="status-dot online"></span>
              <span class="label">Officer</span>
              <strong>Unit {{ myId }}</strong>
            </div>
            <div class="status-chip">
              <span class="label">Badge ID</span>
              <strong>#{{ myId }}</strong>
            </div>
            <div class="status-chip department">
              <span class="label">Department</span>
              <strong>{{ departmentName }}</strong>
            </div>
            <div class="status-chip">
              <span class="label">Active units</span>
              <strong>{{ activeUnits }}</strong>
            </div>
            <div class="status-chip dispatch">
              <img class="indicator-icon" src="/img/mdt/local-light.svg" alt="" />
              <span class="status-dot pulse"></span>
              <span class="label">Dispatch</span>
              <strong>Online</strong>
            </div>
          </div>
          <div class="personal-digital_header-time">13:20</div>
          <img src="/img/personalDigitalAssistant/notification.svg" alt="" />
          <button @click="exit">Go out</button>
        </div>
      </div>
      <div class="header">
        <div
          class="block-nav"
          v-for="(elem, index) in 5"
          :key="index"
          @click="pushCode(item - 1)"
          :class="{ active: code === index }"
        >
          <img src="/img/personalDigitalAssistant/code.png" alt="" />
          <span>Code&nbsp;-&nbsp;{{ index }}</span>
        </div>
      </div>
      <div class="body">
        <div class="nav">
          <div class="block-nav" v-if="type !== 2">
            <nav-item v-for="item in navItems" :item="item" :key="item.id" />
            <div class="mdt-light-stack">
              <div class="mdt-light">
                <img src="/img/mdt/local-light.svg" alt="" />
                <span>Local</span>
              </div>
              <div class="mdt-light">
                <img src="/img/mdt/global-light.svg" alt="" />
                <span>Global</span>
              </div>
            </div>
          </div>
          <div class="block-nav" v-else>
            <nav-item
              v-for="item in navItemsArmy"
              :item="item"
              :key="item.id"
            />
            <div class="mdt-light-stack">
              <div class="mdt-light">
                <img src="/img/mdt/local-light.svg" alt="" />
                <span>Local</span>
              </div>
              <div class="mdt-light">
                <img src="/img/mdt/global-light.svg" alt="" />
                <span>Global</span>
              </div>
            </div>
          </div>
        </div>
        <div class="body-wrap">
          <component :is="currentPage" />
        </div>
      </div>
      <!-- <div :class="[{ army: type === 2 }, { fbi: type === 1 }, 'footer']">
        <footer-item v-for="item in 5" :item="item" :key="item.key" />
      </div> -->
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
/* import FooterItem from "./components/FooterItem"; */
import NavItem from "./components/NavItem";
import HelpPage from "./HelpPage";
import FindHumanPage from "./FindHumanPage";
import FindCarPage from "./FindCarPage";
import WantedListPage from "./WantedListPage";
import DataBasePage from "./DataBasePage";

const AddWantedPage = {
  name: "AddWantedPage",
  functional: true,
  render(h) {
    return h("div", { class: "mdt-real-action-page" }, [
      h("div", { class: "mdt-real-action-page__banner" }, [
        h("img", { attrs: { src: "/img/mdt/wanted-add.svg", alt: "" } }),
        h("div", [
          h("span", "Existing Wanted System"),
          h("strong", "Search a citizen, then use Change to set or remove wanted level"),
        ]),
      ]),
      h(FindHumanPage),
    ]);
  },
};

export default {
  name: "PersonalDigitalAssistant",

  components: {
    /*  FooterItem, */
    NavItem,
    HelpPage,
    FindHumanPage,
    FindCarPage,
    WantedListPage,
    DataBasePage,
    AddWantedPage,
  },

  data: function () {
    return {
      navItems: [
        {
          label: "Search Citizen",
          text: "Search Citizen",
          key: "FindHumanPage",
        },
        {
          label: "Find a Car",
          text: "Find a Car",
          key: "FindCarPage",
        },
        {
          label: "Add to wanted list",
          text: "Add to wanted list",
          key: "AddWantedPage",
        },
        {
          label: "Emergency Calls",
          text: "Emergency Calls",
          key: "HelpPage",
        },
        {
          label: "Wanted List",
          text: "Wanted List",
          key: "WantedListPage",
        },
        {
          label: "Arrest Records",
          text: "Arrest Records",
          key: "DataBasePage",
        },
      ],
    };
  },

  methods: {
    exit: function () {
      window.mp.trigger("pda:exit");
    },
    pushCode: function (item) {
      window.mp.trigger("pda:pushCode", item);
    },
  },

  computed: {
    ...mapState("personalDigitalAssistant", [
      "currentPage",
      "type",
      "code",
      "myId",
      "helpList",
    ]),
    ...mapGetters("localization", ["loc"]),
    navItemsArmy: function () {
      let array = this.navItems;
      return array.filter((element) => element.key !== "DataBasePage");
    },
    departmentName: function () {
      if (this.type === 1) return "FIB";
      if (this.type === 2) return "NG";
      return "LSPD";
    },
    activeUnits: function () {
      const helpers = this.helpList.reduce(
        (count, item) => count + (item.helpers ? item.helpers.length : 0),
        0
      );
      return Math.max(1, helpers);
    },
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.personal-digital-assistant {
  display: flex;
  width: conv(1332);
  height: conv(961);
  padding: conv(86) conv(88) conv(70) conv(92);
  background-size: contain;
  background-position: center;
  background-repeat: no-repeat;
  background-image: url("/img/personalDigitalAssistant/ipad.png");
  color: #fff;
  z-index: 1;

  &__display {
    width: 100%;
    height: 100%;
    display: flex;
    flex-flow: column;
    overflow: hidden;
    border-radius: 1.2rem;
    background: radial-gradient(circle at 12% 8%, rgba(39, 129, 255, 0.2), transparent 32%), radial-gradient(circle at 88% 12%, rgba(61, 196, 255, 0.1), transparent 34%), linear-gradient(135deg, rgba(12, 15, 28, 0.96), rgba(13, 16, 27, 0.9) 46%, rgba(7, 9, 16, 0.96));
    border: 1px solid rgba(255, 255, 255, 0.08);
    box-shadow: inset 0 0 0 1px rgba(255, 255, 255, 0.025), 0 1.1rem 3.5rem rgba(0, 0, 0, 0.34);
  }

  .personal-digital_header {
    height: conv(104);
    min-height: conv(104);
    padding: 0 conv(28);
    display: flex;
    width: 100%;
    align-items: center;
    justify-content: space-between;
    border-bottom: 1px solid rgba(255, 255, 255, 0.075);
    background: linear-gradient(90deg, rgba(255, 255, 255, 0.045), rgba(255, 255, 255, 0.012));

    .head {
      display: flex;
      align-items: center;
      font-weight: 800;
      font-size: conv(25);
      line-height: conv(27);
      letter-spacing: 0.04em;
      text-transform: uppercase;
      color: #ffffff;

      .icon {
        height: conv(58);
        width: conv(58);
        margin-right: conv(18);
        background-size: contain;
        background-position: center;
        background-repeat: no-repeat;
        filter: drop-shadow(0 0 0.9rem rgba(31, 139, 255, 0.38));
      }

      &__info {
        display: flex;
        flex-flow: column;
      }
    }

    & > div:last-child {
      display: flex;
      align-items: center;
      gap: conv(20);

      img {
        display: block;
        height: conv(26);
        opacity: 0.76;
      }

      button {
        display: flex;
        align-items: center;
        justify-content: center;
        height: conv(42);
        min-width: conv(118);
        padding: 0 conv(18);
        cursor: pointer;
        border: 1px solid rgba(255, 255, 255, 0.09);
        border-radius: conv(8);
        background: rgba(255, 255, 255, 0.045);
        transition: 0.18s ease;
        font-weight: 800;
        font-size: conv(14);
        line-height: conv(16);
        letter-spacing: 0.045em;
        text-align: center;
        text-transform: uppercase;
        color: rgba(255, 255, 255, 0.86);

        &:hover {
          background: rgba(31, 139, 255, 0.2);
          border-color: rgba(31, 139, 255, 0.42);
          box-shadow: 0 0 1.2rem rgba(31, 139, 255, 0.16);
        }
      }
    }

    &-time {
      font-weight: 800;
      font-size: conv(15);
      line-height: conv(18);
      letter-spacing: 0.08em;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.78);
    }
  }

  .header {
    width: 100%;
    display: grid;
    grid-template-columns: repeat(5, 1fr);
    gap: conv(12);
    height: conv(58);
    min-height: conv(58);
    padding: conv(14) conv(28) 0;
    margin-bottom: conv(18);

    .block-nav {
      display: flex;
      justify-content: center;
      align-items: center;
      height: conv(44);
      font-weight: 800;
      font-size: conv(14);
      line-height: conv(16);
      letter-spacing: 0.055em;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.78);
      background: rgba(255, 255, 255, 0.035);
      border: 1px solid rgba(255, 255, 255, 0.065);
      border-radius: conv(9);
      transition: 0.18s ease;
      position: relative;
      overflow: hidden;

      img,
      span {
        z-index: 3;
        position: relative;
      }

      &::after {
        z-index: 2;
        content: "";
        position: absolute;
        inset: 0;
        opacity: 0;
        background: linear-gradient(135deg, rgba(31, 139, 255, 0.45), rgba(40, 180, 255, 0.16));
        transition: 0.18s ease;
      }

      &:hover {
        color: #fff;
        border-color: rgba(31, 139, 255, 0.28);
        background: rgba(31, 139, 255, 0.11);
      }

      &.active::after {
        opacity: 1;
      }

      img {
        margin-right: conv(10);
        height: conv(18);
        opacity: 0.86;
      }
    }
  }

  .body {
    flex: 1;
    min-height: 0;
    width: 100%;
    display: grid;
    grid-template-columns: conv(238) 1fr;
    column-gap: conv(22);
    padding: 0 conv(28) conv(28);

    .nav {
      width: 100%;
      display: flex;
      flex-direction: column;
      min-height: 0;

      .block-nav {
        width: 100%;
        display: flex;
        flex-direction: column;
        gap: conv(10);
      }
    }

    .body-wrap {
      min-width: 0;
      min-height: 0;
      height: 100%;
      padding: conv(22);
      border-radius: conv(14);
      border: 1px solid rgba(255, 255, 255, 0.08);
      background: rgba(255, 255, 255, 0.035);
      box-shadow: inset 0 0 0 1px rgba(255, 255, 255, 0.02), 0 0.8rem 2.4rem rgba(0, 0, 0, 0.2);
      overflow: hidden;
    }
  }
}

/* Premium MDT atmosphere pass */
.personal-digital-assistant {
  &__display {
    position: relative;
    background:
      radial-gradient(circle at 16% 4%, rgba(43, 145, 255, 0.26), transparent 30%),
      radial-gradient(circle at 78% 9%, rgba(0, 232, 255, 0.13), transparent 27%),
      linear-gradient(135deg, rgba(5, 13, 28, 0.98), rgba(8, 19, 36, 0.95) 48%, rgba(3, 7, 15, 0.98));
    border-color: rgba(87, 173, 255, 0.18);
    box-shadow:
      inset 0 0 0 1px rgba(255, 255, 255, 0.035),
      inset 0 0 4rem rgba(0, 131, 255, 0.07),
      0 1.35rem 4rem rgba(0, 0, 0, 0.42),
      0 0 2rem rgba(36, 137, 255, 0.11);

    &::before {
      content: "";
      pointer-events: none;
      position: absolute;
      inset: 0;
      z-index: 0;
      opacity: 0.19;
      background-image:
        linear-gradient(rgba(255, 255, 255, 0.035) 1px, transparent 1px),
        linear-gradient(90deg, rgba(255, 255, 255, 0.025) 1px, transparent 1px);
      background-size: conv(42) conv(42);
      mask-image: linear-gradient(180deg, rgba(0, 0, 0, 0.95), rgba(0, 0, 0, 0.45));
    }

    &::after {
      content: "";
      pointer-events: none;
      position: absolute;
      inset: 0;
      z-index: 0;
      opacity: 0.18;
      background: linear-gradient(180deg, transparent 0, rgba(255, 255, 255, 0.045) 50%, transparent 100%);
      background-size: 100% conv(6);
    }

    > * {
      position: relative;
      z-index: 1;
    }
  }

  .personal-digital_header {
    height: conv(118);
    min-height: conv(118);
    padding: conv(14) conv(22) conv(12) conv(24);
    background:
      linear-gradient(90deg, rgba(42, 142, 255, 0.14), rgba(255, 255, 255, 0.025) 34%, rgba(0, 0, 0, 0.08)),
      rgba(255, 255, 255, 0.025);
    border-bottom-color: rgba(102, 183, 255, 0.16);

    .head {
      .icon {
        height: conv(64);
        width: conv(64);
        margin-right: conv(16);
        filter: drop-shadow(0 0 1rem rgba(32, 154, 255, 0.55));
      }

      .title {
        font-size: conv(22);
        line-height: conv(23);
        letter-spacing: 0.075em;
        text-shadow: 0 0 conv(16) rgba(65, 165, 255, 0.22);
      }

      .terminal-subtitle {
        margin-top: conv(5);
        font-weight: 800;
        font-size: conv(10);
        line-height: conv(12);
        letter-spacing: 0.22em;
        color: rgba(107, 198, 255, 0.78);
      }
    }

    .terminal-status {
      min-width: conv(618);
      gap: conv(12);
    }

    .terminal-status__grid {
      display: grid;
      grid-template-columns: conv(132) conv(92) conv(104) conv(105) conv(102);
      gap: conv(7);
      align-items: stretch;
    }

    .status-chip {
      min-height: conv(42);
      padding: conv(7) conv(9);
      display: flex;
      flex-direction: column;
      justify-content: center;
      position: relative;
      overflow: hidden;
      border-radius: conv(8);
      border: 1px solid rgba(111, 190, 255, 0.13);
      background:
        linear-gradient(135deg, rgba(45, 139, 255, 0.12), rgba(255, 255, 255, 0.035)),
        rgba(4, 12, 23, 0.34);
      box-shadow: inset 0 0 1.2rem rgba(39, 139, 255, 0.035);

      &::after {
        content: "";
        position: absolute;
        inset: 0;
        opacity: 0;
        background: linear-gradient(90deg, transparent, rgba(120, 214, 255, 0.12), transparent);
        transform: translateX(-100%);
        animation: statusSweep 4.5s ease-in-out infinite;
      }

      .label {
        font-size: conv(9);
        line-height: conv(11);
        letter-spacing: 0.14em;
        text-transform: uppercase;
        color: rgba(255, 255, 255, 0.42);
      }

      strong {
        margin-top: conv(3);
        font-size: conv(13);
        line-height: conv(15);
        letter-spacing: 0.07em;
        text-transform: uppercase;
        color: rgba(255, 255, 255, 0.94);
      }

      .indicator-icon {
        position: absolute;
        top: conv(7);
        left: conv(7);
        width: conv(15);
        height: conv(15);
        object-fit: contain;
        filter: drop-shadow(0 0 conv(9) rgba(86, 194, 255, 0.55));
      }

      &.dispatch strong {
        color: #74ffbd;
      }

      &.dispatch {
        padding-left: conv(27);
      }
    }

    .status-dot {
      position: absolute;
      top: conv(8);
      right: conv(8);
      width: conv(7);
      height: conv(7);
      border-radius: 999px;
      background: #5effa8;
      box-shadow: 0 0 conv(10) rgba(94, 255, 168, 0.8);
    }

    .status-dot.pulse {
      animation: mdtPulse 1.45s ease-in-out infinite;
    }
  }

  .header {
    height: conv(50);
    min-height: conv(50);
    padding: conv(10) conv(24) 0;
    margin-bottom: conv(12);
    gap: conv(9);

    .block-nav {
      height: conv(38);
      border-radius: conv(7);
      background: rgba(7, 23, 41, 0.48);
      border-color: rgba(98, 183, 255, 0.11);
      box-shadow: inset 0 0 0.9rem rgba(0, 0, 0, 0.1);

      &.active {
        box-shadow: 0 0 1.1rem rgba(36, 147, 255, 0.18), inset 0 0 1.1rem rgba(36, 147, 255, 0.08);
      }
    }
  }

  .body {
    grid-template-columns: conv(214) 1fr;
    column-gap: conv(16);
    padding: 0 conv(24) conv(22);

    .body-wrap {
      padding: conv(16);
      border-radius: conv(12);
      border-color: rgba(96, 182, 255, 0.14);
      background:
        radial-gradient(circle at 100% 0, rgba(31, 139, 255, 0.1), transparent 34%),
        linear-gradient(180deg, rgba(255, 255, 255, 0.042), rgba(255, 255, 255, 0.018)),
        rgba(3, 12, 24, 0.48);
      box-shadow:
        inset 0 0 0 1px rgba(255, 255, 255, 0.025),
        inset 0 0 2.8rem rgba(0, 82, 160, 0.08),
        0 0.8rem 2.2rem rgba(0, 0, 0, 0.24);
    }
  }
}

@keyframes mdtPulse {
  0%, 100% {
    opacity: 0.62;
    transform: scale(0.78);
  }
  50% {
    opacity: 1;
    transform: scale(1.25);
  }
}

@keyframes statusSweep {
  0%, 70% {
    opacity: 0;
    transform: translateX(-100%);
  }
  82% {
    opacity: 1;
  }
  100% {
    opacity: 0;
    transform: translateX(100%);
  }
}
</style>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}
.personal-digital-assistant {
  * {
    font-family: "Akrobat";
    box-sizing: border-box;
  }

  position: relative;

  .btn {
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
    transition: 0.18s ease;
    border-radius: conv(8);
    cursor: pointer;
    user-select: none;
  }

  .scroll {
    &::-webkit-scrollbar {
      width: conv(4);
      background: rgba(255, 255, 255, 0.05);
      border-radius: 999px;
    }

    &::-webkit-scrollbar-thumb {
      background: rgba(31, 139, 255, 0.55);
      border-radius: 999px;
      box-shadow: 0 0 0.6rem rgba(31, 139, 255, 0.25);
    }
  }

  .modal-wrap {
    position: absolute;
    top: 50%;
    left: 50%;
    z-index: 10;
    transform: translate(-50%, -50%);
    display: flex;
    align-items: center;
    justify-content: center;
    width: conv(1238);
    height: conv(863);
    background: rgba(7, 10, 18, 0.94);
    backdrop-filter: blur(0.7rem);
  }
}

/* Asset-backed MDT reference recreation pass */
.personal-digital-assistant {
  &__display {
    background:
      linear-gradient(135deg, rgba(1, 7, 18, 0.42), rgba(1, 10, 25, 0.82)),
      url("/img/mdt/background.png") center / cover no-repeat,
      linear-gradient(135deg, #050d1d, #07182d 52%, #020711) !important;
    border: 1px solid rgba(77, 179, 255, 0.28) !important;
    box-shadow:
      inset 0 0 0 1px rgba(255, 255, 255, 0.045),
      inset 0 0 5.8rem rgba(23, 104, 196, 0.16),
      inset 0 0 12rem rgba(0, 0, 0, 0.45),
      0 1.4rem 4.2rem rgba(0, 0, 0, 0.55),
      0 0 3rem rgba(39, 150, 255, 0.16) !important;

    &::before {
      opacity: 0.32 !important;
      background-image:
        linear-gradient(rgba(91, 190, 255, 0.055) 1px, transparent 1px),
        linear-gradient(90deg, rgba(91, 190, 255, 0.045) 1px, transparent 1px),
        url("/img/mdt/background%202.png") !important;
      background-size: conv(34) conv(34), conv(34) conv(34), cover !important;
      background-position: center !important;
      mix-blend-mode: screen !important;
      mask-image: linear-gradient(180deg, rgba(0, 0, 0, 0.9), rgba(0, 0, 0, 0.28)) !important;
    }

    &::after {
      opacity: 0.26 !important;
      background:
        linear-gradient(180deg, transparent 0, rgba(124, 213, 255, 0.08) 48%, transparent 100%),
        radial-gradient(circle at 20% 22%, rgba(58, 170, 255, 0.18), transparent 24%),
        radial-gradient(circle at 92% 78%, rgba(119, 73, 255, 0.12), transparent 26%) !important;
      background-size: 100% conv(5), cover, cover !important;
      animation: mdtScanline 5.5s linear infinite !important;
    }
  }

  .personal-digital_header {
    height: conv(124) !important;
    min-height: conv(124) !important;
    margin: conv(10) conv(12) 0 !important;
    width: calc(100% - #{conv(24)}) !important;
    border: 1px solid rgba(87, 186, 255, 0.18) !important;
    border-radius: conv(14) !important;
    background:
      linear-gradient(90deg, rgba(30, 125, 230, 0.2), rgba(7, 20, 39, 0.66) 42%, rgba(7, 10, 25, 0.52)),
      rgba(3, 11, 24, 0.62) !important;
    box-shadow:
      inset 0 0 0 1px rgba(255, 255, 255, 0.035),
      inset 0 0 conv(42) rgba(34, 137, 255, 0.09),
      0 0 conv(28) rgba(26, 123, 220, 0.12) !important;

    .head .title {
      font-size: conv(25) !important;
      line-height: conv(25) !important;
    }

    .head .terminal-subtitle {
      color: rgba(134, 215, 255, 0.92) !important;
    }

    .terminal-status__grid {
      grid-template-columns: conv(144) conv(100) conv(112) conv(116) conv(112) !important;
    }

    .status-chip {
      background:
        linear-gradient(135deg, rgba(42, 148, 255, 0.16), rgba(255, 255, 255, 0.035)),
        rgba(1, 9, 21, 0.68) !important;
      border-color: rgba(105, 203, 255, 0.2) !important;
      box-shadow:
        inset 0 0 conv(20) rgba(53, 158, 255, 0.075),
        0 0 conv(14) rgba(0, 0, 0, 0.18) !important;
    }
  }

  .header {
    padding: conv(9) conv(24) 0 !important;
    margin-bottom: conv(10) !important;

    .block-nav {
      background:
        linear-gradient(135deg, rgba(14, 46, 88, 0.58), rgba(3, 11, 24, 0.78)) !important;
      border-color: rgba(88, 186, 255, 0.16) !important;
      clip-path: polygon(conv(8) 0, 100% 0, calc(100% - #{conv(8)}) 100%, 0 100%) !important;

      &.active::after {
        background: linear-gradient(135deg, rgba(29, 138, 255, 0.72), rgba(104, 77, 255, 0.35)) !important;
      }
    }
  }

  .body {
    grid-template-columns: conv(226) 1fr !important;
    column-gap: conv(15) !important;
    padding: 0 conv(24) conv(20) !important;

    .body-wrap {
      padding: conv(14) !important;
      background:
        linear-gradient(135deg, rgba(255, 255, 255, 0.055), rgba(255, 255, 255, 0.018)),
        rgba(1, 10, 23, 0.72) !important;
      border-color: rgba(88, 186, 255, 0.22) !important;
      box-shadow:
        inset 0 0 0 1px rgba(255, 255, 255, 0.035),
        inset 0 0 conv(44) rgba(31, 139, 255, 0.105),
        0 0 conv(30) rgba(0, 0, 0, 0.28) !important;
      position: relative !important;

      &::before,
      &::after {
        content: "";
        pointer-events: none;
        position: absolute;
        width: conv(42);
        height: conv(42);
        border-color: rgba(95, 199, 255, 0.5);
        opacity: 0.8;
      }

      &::before {
        top: conv(8);
        left: conv(8);
        border-top: 1px solid;
        border-left: 1px solid;
      }

      &::after {
        right: conv(8);
        bottom: conv(8);
        border-right: 1px solid;
        border-bottom: 1px solid;
      }
    }
  }
}

@keyframes mdtScanline {
  from { background-position: 0 0, center, center; }
  to { background-position: 0 conv(42), center, center; }
}

/* PDF-layout correction pass: fullscreen CAD overlay */
.personal-digital-assistant {
  width: 100vw !important;
  height: 100vh !important;
  padding: 0 !important;
  background: none !important;
  background-image: none !important;
  align-items: stretch !important;
  justify-content: stretch !important;

  &__display {
    width: 100vw !important;
    height: 100vh !important;
    border-radius: 0 !important;
    border: 0 !important;
    overflow: hidden !important;
    background:
      linear-gradient(90deg, rgba(0, 0, 0, 0.18), rgba(3, 7, 18, 0.36) 32%, rgba(0, 0, 0, 0.08)),
      linear-gradient(180deg, rgba(0, 8, 22, 0.18), rgba(0, 0, 0, 0.26)),
      url("/img/mdt/background.png") center / cover no-repeat !important;
    box-shadow: inset 0 0 0 1px rgba(83, 188, 255, 0.1), inset 0 0 12rem rgba(0, 0, 0, 0.58) !important;

    &::before {
      opacity: 0.38 !important;
      background-image:
        url("/img/mdt/background%202.png"),
        linear-gradient(rgba(86, 192, 255, 0.07) 1px, transparent 1px),
        linear-gradient(90deg, rgba(86, 192, 255, 0.055) 1px, transparent 1px) !important;
      background-size: cover, conv(38) conv(38), conv(38) conv(38) !important;
      background-position: center, center, center !important;
      mix-blend-mode: screen !important;
    }
  }

  .personal-digital_header {
    height: conv(92) !important;
    min-height: conv(92) !important;
    width: calc(100% - #{conv(52)}) !important;
    margin: conv(22) conv(26) 0 !important;
    padding: conv(10) conv(18) !important;
    border-radius: conv(6) !important;
    border: 1px solid rgba(90, 188, 255, 0.18) !important;
    background: linear-gradient(90deg, rgba(13, 28, 60, 0.68), rgba(4, 10, 25, 0.4), rgba(7, 12, 28, 0.58)) !important;
    box-shadow: inset 0 0 conv(28) rgba(34, 137, 255, 0.08), 0 0 conv(28) rgba(20, 83, 160, 0.14) !important;

    .head .icon {
      width: conv(48) !important;
      height: conv(48) !important;
      margin-right: conv(12) !important;
    }

    .head .title {
      font-size: conv(19) !important;
      line-height: conv(19) !important;
    }

    .terminal-subtitle {
      font-size: conv(8) !important;
      line-height: conv(10) !important;
    }

    .terminal-status {
      min-width: auto !important;
      gap: conv(9) !important;
    }

    .terminal-status__grid {
      grid-template-columns: conv(118) conv(82) conv(92) conv(96) conv(96) !important;
      gap: conv(6) !important;
    }

    .status-chip {
      min-height: conv(36) !important;
      padding: conv(5) conv(8) !important;
      border-radius: conv(4) !important;
    }
  }

  .header {
    display: none !important;
  }

  .body {
    flex: 1 !important;
    height: calc(100vh - #{conv(122)}) !important;
    display: grid !important;
    grid-template-columns: conv(328) 1fr !important;
    column-gap: conv(34) !important;
    padding: conv(30) conv(44) conv(42) conv(34) !important;
  }

  .body .nav {
    position: relative !important;
    padding-left: conv(18) !important;

    &::before {
      content: "";
      position: absolute;
      left: conv(6);
      top: conv(4);
      bottom: conv(86);
      width: 1px;
      background: linear-gradient(180deg, transparent, rgba(106, 199, 255, 0.68), rgba(112, 69, 255, 0.5), transparent);
      box-shadow: 0 0 conv(14) rgba(83, 188, 255, 0.5);
    }

    .block-nav {
      height: 100% !important;
      gap: conv(13) !important;
      justify-content: flex-start !important;
    }
  }

  .mdt-light-stack {
    margin-top: auto;
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: conv(10);
    padding-top: conv(18);
  }

  .mdt-light {
    height: conv(64);
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    gap: conv(4);
    border-radius: conv(8);
    border: 1px solid rgba(102, 202, 255, 0.18);
    background: rgba(4, 14, 31, 0.36);
    box-shadow: inset 0 0 conv(18) rgba(34, 137, 255, 0.06), 0 0 conv(16) rgba(0, 0, 0, 0.18);

    img {
      width: conv(31);
      height: conv(31);
      object-fit: contain;
      filter: drop-shadow(0 0 conv(12) rgba(92, 218, 255, 0.7));
    }

    span {
      font-size: conv(9);
      line-height: conv(11);
      font-weight: 900;
      letter-spacing: 0.16em;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.72);
    }
  }

  .body .body-wrap {
    max-width: conv(1180) !important;
    width: 100% !important;
    justify-self: center !important;
    padding: conv(26) conv(30) !important;
    border-radius: conv(8) !important;
    border: 1px solid rgba(97, 188, 255, 0.24) !important;
    background: linear-gradient(135deg, rgba(4, 13, 31, 0.76), rgba(5, 11, 25, 0.46)) !important;
    backdrop-filter: blur(conv(4)) !important;
    box-shadow: inset 0 0 0 1px rgba(255, 255, 255, 0.035), inset 0 0 conv(50) rgba(59, 156, 255, 0.08), 0 0 conv(38) rgba(0, 0, 0, 0.24) !important;
  }

  .mdt-real-action-page {
    height: 100%;
    display: flex;
    flex-direction: column;
    min-height: 0;
    overflow: hidden;
    text-transform: uppercase;
    color: #fff;
  }

  .mdt-real-action-page__banner {
    margin-bottom: conv(16);
    min-height: conv(74);
    padding: conv(12) conv(16);
    display: flex;
    align-items: center;
    gap: conv(14);
    border-radius: conv(7);
    border: 1px solid rgba(102, 202, 255, 0.18);
    background:
      linear-gradient(90deg, rgba(101, 65, 224, 0.22), rgba(36, 125, 230, 0.11)),
      rgba(0, 8, 20, 0.36);
    box-shadow: inset 0 0 conv(22) rgba(77, 176, 255, 0.08), 0 0 conv(18) rgba(0, 0, 0, 0.18);

    img {
      width: conv(38);
      height: conv(38);
      object-fit: contain;
      filter: drop-shadow(0 0 conv(14) rgba(112, 215, 255, 0.58));
    }

    span {
      display: block;
      margin-bottom: conv(5);
      font-size: conv(10);
      line-height: conv(12);
      font-weight: 900;
      letter-spacing: 0.18em;
      color: rgba(124, 212, 255, 0.82);
    }

    strong {
      display: block;
      font-size: conv(15);
      line-height: conv(18);
      font-weight: 900;
      letter-spacing: 0.08em;
      color: rgba(255, 255, 255, 0.88);
    }
  }
}
</style>
