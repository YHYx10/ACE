<template>
  <div class="wanted-list-page">
    <div class="wanted-list-page__nav">
      <div
        :class="[
          { active: item.key === currentSearch },
          { army: type === 2 },
          { fbi: type === 1 },
          'btn',
        ]"
        v-for="item in navList"
        :key="item.id"
        @click="setCurrentSearch(item.key)"
      >
        <span>{{ loc(item.text) }}</span>
      </div>
    </div>
    <template v-if="currentSearch === 'humans'">
      <div class="wanted-list-page__head nav-humans">
        <div class="head-item" v-for="item in humansNav" :key="item.id">
          {{ loc(item) }}
        </div>
      </div>
      <div class="wanted-list-page__body body-humans scroll">
        <div class="body-item" v-for="item in humanListWanted" :key="item.key">
          <div>{{ item.name }}</div>
          <div>{{ item.passport }}</div>
          <div>{{ loc(item.sex ? "Pda_28" : "Pda_29") }}</div>
          <div>{{ item.number }}</div>
          <div class="wanted">
            <div
              :class="[{ active: index + 1 <= item.wantedLevel }, 'star']"
              v-for="(star, index) in 5"
              :key="star.key"
            >
              <svg
                v-if="index + 1 <= item.wantedLevel"
                width="20"
                height="19"
                viewBox="0 0 20 19"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  clip-rule="evenodd"
                  d="M20 7.244L12.809 6.627L10 0L7.191 6.627L0 7.244L5.455 11.971L3.82 19L10 15.272L16.18 19L14.545 11.971L20 7.244Z"
                  fill="#FFDF6D"
                />
              </svg>
              <svg
                v-else
                width="20"
                height="19"
                viewBox="0 0 20 19"
                :fill="'none'"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  clip-rule="evenodd"
                  d="M20 7.244L12.809 6.627L10 0L7.191 6.627L0 7.244L5.455 11.971L3.82 19L10 15.272L16.18 19L14.545 11.971L20 7.244ZM10 13.396L6.237 15.666L7.233 11.385L3.91 8.507L8.29 8.131L10 4.095L11.71 8.131L16.09 8.507L12.768 11.385L13.764 15.666L10 13.396Z"
                  fill="white"
                  fill-opacity="0.1"
                />
              </svg>
            </div>
          </div>
          <div class="licenses">{{ item.licenses }}</div>
          <div>
            <div
              :class="[
                { army: type === 2 },
                { fbi: type === 1 },
                'btn',
                'btn-remove-wanted',
              ]"
              @click="
                editSearchWantedLevel(
                  'player',
                  item.passport,
                  item.wantedLevel,
                  item.reason
                )
              "
            >
              {{ loc("Pda_4") }}
              <!-- <svg 
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24" 
                fill="none"
                class="icon"
              >
                <path d="M3 17.2501V21.0001H6.75L17.81 9.94006L14.06 6.19006L3 17.2501ZM20.71 7.04006C21.1 6.65006 21.1 6.02006 20.71 5.63006L18.37 3.29006C17.98 2.90006 17.35 2.90006 16.96 3.29006L15.13 5.12006L18.88 8.87006L20.71 7.04006Z"/>
              </svg> -->
            </div>
            <!-- <div 
              :class="[{army: type === 2}, {fbi: type === 1}, 'btn', 'btn-remove-wanted']"
              @click="pushToBase(item.id)"
            >move to Data base</div> -->
          </div>
        </div>
      </div>
    </template>
    <template v-else>
      <div class="wanted-list-page__head nav-cars">
        <div class="head-item" v-for="item in carsNav" :key="item.id">
          {{ loc(item) }}
        </div>
      </div>
      <div class="wanted-list-page__body body-cars scroll">
        <div class="body-item" v-for="item in carListWanted" :key="item.id">
          <div>{{ item.name }}</div>
          <div>{{ item.number }}</div>
          <div>{{ item.carModel }}</div>
          <div class="wanted">
            <div
              :class="[{ active: index + 1 <= item.wantedLevel }, 'star']"
              v-for="(star, index) in 5"
              :key="star.key"
            >
              <svg
                v-if="index + 1 <= item.wantedLevel"
                width="20"
                height="19"
                viewBox="0 0 20 19"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  clip-rule="evenodd"
                  d="M20 7.244L12.809 6.627L10 0L7.191 6.627L0 7.244L5.455 11.971L3.82 19L10 15.272L16.18 19L14.545 11.971L20 7.244Z"
                  fill="#FFDF6D"
                />
              </svg>
              <svg
                v-else
                width="20"
                height="19"
                viewBox="0 0 20 19"
                :fill="'none'"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  clip-rule="evenodd"
                  d="M20 7.244L12.809 6.627L10 0L7.191 6.627L0 7.244L5.455 11.971L3.82 19L10 15.272L16.18 19L14.545 11.971L20 7.244ZM10 13.396L6.237 15.666L7.233 11.385L3.91 8.507L8.29 8.131L10 4.095L11.71 8.131L16.09 8.507L12.768 11.385L13.764 15.666L10 13.396Z"
                  fill="white"
                  fill-opacity="0.1"
                />
              </svg>
            </div>
          </div>
          <div>
            <div
              :class="[
                { army: type === 2 },
                { fbi: type === 1 },
                'btn',
                'btn-remove-wanted',
              ]"
              @click="
                editSearchWantedLevel(
                  'car',
                  item.key,
                  item.wantedLevel,
                  item.reason
                )
              "
            >
              {{ loc("Pda_4") }}
            </div>
          </div>
        </div>
      </div>
    </template>
    <modal-wanted
      v-if="modalShow"
      :typeModal="currentType"
      :id="currentId"
      :wantedLevel="currentWantedLevel"
      :reason="currentReason"
      @closeModal="closeModal"
    />
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
import ModalWanted from "../components/ModalWanted";

export default {
  name: "WantedListPage",

  components: {
    ModalWanted,
  },

  data: function () {
    return {
      currentSearch: null,
      modalShow: false,
      currentType: null,
      currentId: null,
      currentWantedLevel: null,
      currentReason: null,
      navList: [
        {
          key: "humans",
          text: "Pda_12",
        },
        {
          key: "cars",
          text: "Pda_13",
        },
      ],
      carsNav: ["Pda_14", "Pda_15", "Pda_16", "Pda_17"],
      humansNav: ["Pda_14", "Pda_18", "Pda_19", "Pda_20", "Pda_17", "Pda_21"],
    };
  },

  computed: {
    ...mapState("personalDigitalAssistant", [
      "humanListWanted",
      "carListWanted",
      "type",
    ]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    closeModal: function () {
      this.modalShow = false;
    },
    setCurrentSearch: function (key) {
      this.currentSearch = key;
    },
    editSearchWantedLevel: function (
      currentType,
      currentId,
      currentWantedLevel,
      currentReason
    ) {
      this.modalShow = true;
      this.currentType = currentType;
      this.currentId = currentId;
      this.currentWantedLevel = currentWantedLevel;
      this.currentReason = currentReason;
    },
    pushToBase: function (id) {
      window.mp.trigger("pda:pushToBase", id);
    },
  },

  created() {
    this.setCurrentSearch("humans");
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.wanted-list-page {
  display: flex;
  flex-flow: column;
  width: 100%;
  height: 100%;

  &__nav {
    display: flex;
    align-items: center;
    margin-bottom: conv(24);

    .btn {
      height: conv(40);
      width: conv(120);
      display: flex;
      justify-content: center;
      align-items: center;
      cursor: pointer;
      transition: 0.2s ease;
      position: relative;
      font-weight: 700;
      font-size: conv(13);
      line-height: conv(16);
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;
      background: rgba(255, 255, 255, 0.03);

      &::after {
        content: "";
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        opacity: 0;
        transition: 0.2s ease;
        background: linear-gradient(
          180deg,
          #301934  0%,
          #591b87 100%,
          #591b87 100%
        );
        z-index: 2;
      }

      &:not(:last-child) {
        margin-right: conv(10);
      }

      &:hover {
        background: rgba(255, 255, 255, 0.07);
      }

      &.active::after {
        opacity: 1;
      }

      span {
        z-index: 3;
        position: relative;
      }
    }
  }

  &__head {
    padding-left: conv(20);
    display: grid;
    column-gap: conv(20);
    align-items: center;
    font-weight: 700;
    font-size: conv(15);
    line-height: conv(18);
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.5);
    margin-bottom: conv(16);
  }

  &__head.nav-humans {
    grid-template-columns: conv(190) conv(60) conv(26) conv(95) conv(117) auto;
  }

  &__head.nav-cars {
    grid-template-columns: 9.5rem 4.3rem 9.75rem auto;
  }

  &__body {
    display: flex;
    flex-direction: column;
    max-height: conv(412);
    padding-right: conv(10);
    overflow-y: auto;
    overflow-x: hidden;
    width: 100%;

    .body-item {
      width: 100%;
      display: grid;
      column-gap: conv(20);
      align-items: center;
      border: 1px solid rgba(255, 255, 255, 0.09);
      font-weight: 700;
      font-size: conv(18);
      line-height: conv(22);
      text-transform: uppercase;
      color: #ffffff;
      padding-left: conv(20);

      .wanted {
        display: flex;
        align-items: center;

        .star {
          height: conv(19);

          &:not(:last-child) {
            margin-right: conv(2);
          }

          img {
            height: 100%;
            width: conv(20);
          }
        }
      }

      & > div {
        display: flex;
        align-items: center;
        height: 100%;

        &:last-child {
          justify-content: flex-end;
        }
      }

      .btn-wanted,
      .btn-remove-wanted {
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
      }

      &:not(:last-child) {
        margin-bottom: conv(3);
      }
    }
  }

  &__body.body-cars .body-item {
    grid-template-columns: conv(190) conv(86) conv(195) auto auto;
    height: conv(80);
    min-height: conv(80);
  }

  &__body.body-humans .body-item {
    grid-template-columns: conv(190) conv(60) conv(26) conv(95) conv(117) auto auto;

    .licenses{
      min-height: conv(78);
      padding: conv(6) 0;
      width: calc(100% + 1rem);
      padding-right: 0.2rem;
    }
  }

  /*  &__body.body-humans .body-item > div {
    &:nth-child(1) {
      width: 8.85rem;
      min-width: 8.85rem;
    }
    &:nth-child(2) {
      width: 6.7rem;
      min-width: 6.7rem;
    }
    &:nth-child(3) {
      width: 3.45rem;
      min-width: 3.45rem;
    }
    &:nth-child(4) {
      width: 8.35rem;
      min-width: 8.35rem;
    }
    &:nth-child(5) {
      width: 10.2rem;
      min-width: 10.2rem;
    }
    &:nth-child(6) {
      width: 6rem;
      min-width: 6rem;
    }
  } */

  &__body {
    /*  display: flex;
    flex-flow: column;
    width: 100%;
    padding-right: 0.6rem;
    overflow-y: auto;
    height: 100%;
    max-height: 100%; */
    /* &.body-humans {
      .body-item {
        .btn-remove-wanted {
          min-height: 2.9rem;
          margin-right: 0.6rem;
          &:last-child {
            margin-right: 0;
          }
          .icon {
            width: 1.2rem;
            height: 1.2rem;
          }
          &.fbi {
            .icon {
              fill: #fb7712;
            }
          }
          &.army {
            .icon {
              fill: #8da997;
            }
          }
        }
      }
    } */
    /* .body-item {
      width: 100%;
      padding: 0 1rem;
      display: flex;
      align-items: center;
      justify-content: flex-start;
      background: rgba(255, 255, 255, 0.2);
      border-radius: 0.4rem;
      font-size: 1rem;
      height: 4.5rem;
      min-height: 4.5rem;
      margin-bottom: 1rem;
      .wanted,
      .licenses {
        display: flex;
        align-items: center;
      }
      .wanted {
        .star {
          margin-right: 0.5rem;
          width: 1.2rem;
          height: 1.2rem;
          background-size: contain;
          background-repeat: no-repeat;
          background-position: center;
          background-image: url("/img/personalDigitalAssistant/wanted-star.svg");
          &.active {
            background-image: url("/img/personalDigitalAssistant/wanted-star-active.svg");
          }
        }
      }
      & > div {
        display: flex;
        align-items: center;
        &:last-child {
          width: 100%;
          justify-content: flex-end;
        }
      }
      .btn-remove-wanted {
        border-radius: 0.4rem;
        padding: 0.6rem;
        font-size: 0.8rem;
        max-width: 5.7rem;
        background: #333333;
        color: #ffd130;
        width: fit-content;
        text-align: center;
        &:hover {
          transform: scale(1.1);
        }
        &:active {
          transform: scale(1.05);
        }
        &.fbi {
          color: #fb7712;
        }
        &.army {
          color: #8da997;
        }
      }
    } */
  }
}

/* MDT visual restyle */
.wanted-list-page__nav {
  margin-bottom: conv(20) !important;
  gap: conv(10) !important;

  .btn {
    height: conv(36) !important;
    width: conv(132) !important;
    border-radius: conv(9) !important;
    border: 1px solid rgba(255, 255, 255, 0.08) !important;
    background: rgba(255, 255, 255, 0.04) !important;
    font-size: conv(12) !important;
    letter-spacing: 0.065em !important;
    overflow: hidden !important;

    &::after {
      background: linear-gradient(135deg, rgba(31, 139, 255, 0.72), rgba(40, 180, 255, 0.24)) !important;
    }

    &:hover,
    &.active {
      color: #fff !important;
      border-color: rgba(31, 139, 255, 0.38) !important;
      box-shadow: 0 0 1rem rgba(31, 139, 255, 0.14) !important;
    }
  }
}

.wanted-list-page__head {
  padding: 0 conv(16) !important;
  margin-bottom: conv(8) !important;
  font-size: conv(12) !important;
  line-height: conv(15) !important;
  letter-spacing: 0.08em !important;
  color: rgba(255, 255, 255, 0.45) !important;
}

.wanted-list-page__body {
  max-height: conv(470) !important;
  padding-right: conv(8) !important;

  .body-item {
    min-height: conv(68) !important;
    border: 1px solid rgba(255, 255, 255, 0.075) !important;
    border-radius: conv(12) !important;
    background: rgba(255, 255, 255, 0.035) !important;
    padding-left: conv(16) !important;
    font-size: conv(15) !important;
    line-height: conv(18) !important;
    letter-spacing: 0.025em !important;
    margin-bottom: conv(8) !important;
    box-shadow: inset 0 0 0 1px rgba(255, 255, 255, 0.015) !important;

    .btn-wanted,
    .btn-remove-wanted {
      height: conv(42) !important;
      width: conv(108) !important;
      min-width: conv(108) !important;
      margin-right: conv(10) !important;
      border-radius: conv(9) !important;
      background: rgba(31, 139, 255, 0.12) !important;
      border: 1px solid rgba(31, 139, 255, 0.25) !important;
      font-size: conv(12) !important;
      letter-spacing: 0.055em !important;

      &::after {
        background: linear-gradient(135deg, rgba(31, 139, 255, 0.7), rgba(40, 180, 255, 0.24)) !important;
      }
    }
  }
}

/* Premium MDT warrant hierarchy pass */
.wanted-list-page {
  &__nav {
    margin-bottom: conv(12) !important;
    padding-bottom: conv(10) !important;
    border-bottom: 1px solid rgba(94, 185, 255, 0.09) !important;

    .btn {
      width: conv(144) !important;
      height: conv(34) !important;
      background: rgba(6, 19, 36, 0.62) !important;
      border-color: rgba(106, 193, 255, 0.13) !important;
      box-shadow: inset 0 0 conv(14) rgba(31, 139, 255, 0.035) !important;

      &.active {
        background: linear-gradient(135deg, rgba(31, 139, 255, 0.55), rgba(22, 70, 130, 0.38)) !important;
        box-shadow: 0 0 conv(18) rgba(31, 139, 255, 0.2), inset 0 0 conv(16) rgba(255, 255, 255, 0.055) !important;
      }
    }
  }

  &__head {
    color: rgba(155, 212, 255, 0.58) !important;
  }

  &__body {
    max-height: conv(505) !important;

    .body-item {
      position: relative !important;
      min-height: conv(72) !important;
      background:
        radial-gradient(circle at 0 50%, rgba(255, 70, 70, 0.15), transparent 34%),
        linear-gradient(135deg, rgba(255, 255, 255, 0.05), rgba(255, 255, 255, 0.017)),
        rgba(3, 12, 25, 0.66) !important;
      border-color: rgba(255, 95, 95, 0.17) !important;
      border-radius: conv(12) !important;
      overflow: hidden !important;
      box-shadow: inset 0 0 0 1px rgba(255, 255, 255, 0.018), 0 0 conv(22) rgba(0, 0, 0, 0.18) !important;

      &::before {
        content: "ACTIVE WARRANT";
        position: absolute;
        top: conv(7);
        right: conv(126);
        font-weight: 900;
        font-size: conv(8);
        line-height: conv(10);
        letter-spacing: 0.16em;
        color: rgba(255, 116, 116, 0.82);
        text-shadow: 0 0 conv(10) rgba(255, 58, 58, 0.34);
      }

      &::after {
        content: "";
        position: absolute;
        left: 0;
        top: 12%;
        width: conv(4);
        height: 76%;
        border-radius: 999px;
        background: #ff4f5d;
        box-shadow: 0 0 conv(16) rgba(255, 79, 93, 0.65);
      }

      .wanted .star svg path[fill="#FFDF6D"] {
        filter: drop-shadow(0 0 conv(8) rgba(255, 216, 107, 0.55));
      }

      .btn-wanted,
      .btn-remove-wanted {
        height: conv(40) !important;
        width: conv(102) !important;
        min-width: conv(102) !important;
        background: rgba(255, 79, 93, 0.11) !important;
        border-color: rgba(255, 79, 93, 0.3) !important;
        color: #ff9ca5 !important;
        box-shadow: 0 0 conv(16) rgba(255, 79, 93, 0.13) !important;

        &:hover {
          color: #fff !important;
          background: rgba(255, 79, 93, 0.22) !important;
        }
      }
    }
  }
}

/* Asset-backed MDT wanted styling */
.wanted-list-page {
  &__nav {
    position: relative !important;
    min-height: conv(46) !important;
    padding-left: conv(46) !important;

    &::before {
      content: "";
      position: absolute;
      left: conv(8);
      top: conv(2);
      width: conv(30);
      height: conv(30);
      background: url("/img/mdt/wanted-list.svg") center / contain no-repeat;
      filter: drop-shadow(0 0 conv(12) rgba(255, 88, 88, 0.52));
    }
  }

  &__body .body-item {
    background:
      radial-gradient(circle at 0 50%, rgba(255, 68, 85, 0.22), transparent 32%),
      linear-gradient(135deg, rgba(255, 255, 255, 0.055), rgba(255, 255, 255, 0.016)),
      rgba(2, 11, 25, 0.78) !important;
    border-color: rgba(255, 87, 102, 0.24) !important;

    &:hover {
      transform: translateX(conv(2));
      border-color: rgba(255, 108, 121, 0.38) !important;
      box-shadow: inset 0 0 conv(28) rgba(255, 64, 80, 0.08), 0 0 conv(24) rgba(255, 64, 80, 0.12) !important;
    }
  }
}

/* PDF-layout correction pass: wanted table */
.wanted-list-page {
  &::before {
    content: "WANTED LIST";
    height: conv(62);
    margin-bottom: conv(16);
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: conv(34);
    line-height: conv(38);
    font-weight: 900;
    letter-spacing: 0.1em;
    text-transform: uppercase;
    color: #fff;
    text-shadow: 0 0 conv(24) rgba(255, 78, 105, 0.24);
  }

  &__nav {
    justify-content: center !important;
    min-height: conv(42) !important;
    padding-left: 0 !important;
    border-bottom: 0 !important;

    &::before {
      left: calc(50% - #{conv(190)}) !important;
      top: conv(6) !important;
    }
  }

  &__head {
    height: conv(44) !important;
    padding: 0 conv(18) !important;
    align-items: center !important;
    border: 1px solid rgba(255, 112, 128, 0.16) !important;
    background: rgba(0, 0, 0, 0.34) !important;
    margin-bottom: conv(8) !important;
  }

  &__body {
    max-height: calc(100% - #{conv(146)}) !important;
  }

  &__body .body-item {
    border-radius: conv(3) !important;
    min-height: conv(78) !important;
    background: linear-gradient(90deg, rgba(54, 12, 28, 0.7), rgba(18, 30, 68, 0.48), rgba(7, 12, 28, 0.72)) !important;
  }
}
</style>