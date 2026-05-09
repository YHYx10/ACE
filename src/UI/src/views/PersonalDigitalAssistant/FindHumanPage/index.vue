<template>
  <div class="find-human-page">
    <div class="title">
      <div class="text">{{ loc("Pda_2") }}</div>
      <div
        :class="[
          { active: item.key === currentSearch },
          { army: type === 2 },
          { fbi: type === 1 },
          'btn',
        ]"
        v-for="item in searchFilters"
        :key="item.id"
        @click="setCurrentSearch(item.key)"
      >
        <span>{{ loc(item.text) }}</span>
      </div>
    </div>
    <div class="search-wrap">
      <input
        type="text"
        v-model="currentSearchText"
        :placeholder="loc('Pda_32')"
        v-if="currentSearch === 'passport'"
      />
      <input
        type="text"
        v-model="currentSearchText"
        :placeholder="loc('Pda_33')"
        v-else-if="currentSearch === 'nickname'"
      />
      <input
        type="text"
        v-model="currentSearchText"
        :placeholder="loc('Pda_34')"
        v-else
      />
      <div
        class="btn btn-discard"
        @click="discardCurrentSearchText"
        v-if="currentSearchText"
      ></div>
      <div class="btn btn-find" @click="setSearchHuman">FIND</div>
    </div>
    <template v-if="searchHuman.name">
      <div class="search-result">
        <div class="record-dossier">
          <div class="record-avatar">
            <img src="/img/mdt/profile-placeholder.svg" alt="" />
          </div>
          <div class="record-summary">
            <span class="record-kicker">Citizen profile</span>
            <strong>{{ searchHuman.name }}</strong>
            <small>ID #{{ searchHuman.passport }} / {{ searchHuman.number }}</small>
          </div>
          <div class="record-status" :class="{ danger: searchHuman.wantedLevel > 0 }">
            {{ searchHuman.wantedLevel > 0 ? "WANTED" : "CLEAR" }}
          </div>
        </div>
        <div class="search-result__head">
          <div class="text" v-for="item in headNav" :key="item.id">
            {{ loc(item) }}
          </div>
        </div>
        <div class="search-result__body">
          <div>{{ searchHuman.name }}</div>
          <div>{{ searchHuman.passport }}</div>
          <div>{{ loc(searchHuman.sex ? "Pda_28" : "Pda_29") }}</div>
          <div>{{ searchHuman.number }}</div>
          <div class="wanted">
            <div
              :class="[
                { active: index + 1 <= searchHuman.wantedLevel },
                'star',
              ]"
              v-for="(item, index) in 5"
              :key="item.key"
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
          <div class="licenses">{{ searchHuman.licenses }}</div>
          <div>
            <div
              :class="[
                { army: type === 2 },
                { fbi: type === 1 },
                'btn',
                'btn-remove-wanted',
              ]"
              @click="editSearchHumanWantedLevel"
            >
              <!-- v-if="searchHuman.wantedLevel > 0" -->
              change
            </div>
            <!-- <div
              :class="[
                { army: type === 2 },
                { fbi: type === 1 },
                'btn',
                'btn-wanted',
              ]"
              @click="editSearchHumanWantedLevel"
              v-else
            >
              {{ loc("Pda_10") }}
            </div> -->
          </div>
        </div>
      </div>
    </template>
    <modal-wanted
      v-if="modalShow"
      :typeModal="'player'"
      :id="this.searchHuman.passport"
      :wantedLevel="this.searchHuman.wantedLevel"
      @closeModal="closeModal"
    />
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";

import ModalWanted from "../components/ModalWanted";

export default {
  name: "FindHumanPage",

  components: {
    ModalWanted,
  },

  data: function () {
    return {
      currentSearch: "number",
      currentSearchText: null,
      searchFilters: [
        {
          text: "passport",
          key: "passport",
        },
        {
          text: "Full name",
          key: "nickname",
        },
        {
          text: "phone number",
          key: "number",
        },
      ],
      headNav: ["Pda_14", "Pda_18", "Pda_19", "Pda_20", "Pda_17", "Pda_21"],
      modalShow: false,
    };
  },

  computed: {
    ...mapState("personalDigitalAssistant", ["searchHuman", "type"]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    closeModal: function () {
      this.modalShow = false;
    },
    setCurrentSearch: function (key) {
      this.currentSearch = key;
      this.currentSearchText = null;
    },
    discardCurrentSearchText: function () {
      this.currentSearchText = null;
    },
    setSearchHuman: function () {
      window.mp.trigger(
        "pda:setSearchHuman",
        this.currentSearchText,
        this.currentSearch
      );
    },
    editSearchHumanWantedLevel: function () {
      this.modalShow = true;
    },
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.find-human-page {
  height: 100%;
  width: 100%;
  display: flex;
  flex-flow: column;
  justify-content: flex-start;

  .title {
    display: flex;
    align-items: center;
    justify-content: flex-start;
    margin-bottom: conv(14);
    min-height: conv(24);

    .text {
      font-weight: 700;
      font-size: conv(20);
      line-height: conv(24);
      display: flex;
      align-items: center;
      text-transform: uppercase;
      color: #ffffff;
      margin-right: conv(20);
    }

    .btn {
      background: rgba(255, 255, 255, 0.03);
      height: conv(24);
      min-width: conv(73);
      padding: 0 conv(15);
      transition: 0.2s ease;
      position: relative;

      span {
        z-index: 3;
      }

      &::after {
        z-index: 2;
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
      }
      font-weight: 700;
      font-size: conv(13);
      line-height: conv(16);
      display: flex;
      align-items: center;
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;

      &:hover {
        background: rgba(255, 255, 255, 0.07);
      }

      &.active::after {
        opacity: 1;
      }

      &:not(:last-child) {
        margin-right: conv(10);
      }
    }
  }

  .search-wrap {
    width: 100%;
    height: conv(57);
    min-height: conv(57);
    margin-bottom: conv(20);
    display: flex;
    align-items: center;
    justify-content: space-between;
    position: relative;

    input {
      height: 100%;
      width: 100%;
      margin-right: conv(10);
      border: 1px solid rgba(255, 255, 255, 0.09);
      padding-left: conv(20);
      font-weight: 700;
      font-size: conv(15);
      line-height: conv(18);
      
      background: none;
      outline: none;
      color: #ffffff;

      &::placeholder {
        color: rgba(255, 255, 255, 0.31);
        text-transform: uppercase;
      }
    }

    .btn-find {
      cursor: pointer;
      height: 100%;
      display: flex;
      align-items: center;
      justify-content: center;
      transition: 0.2s ease;
      width: conv(139);
      min-width: conv(139);
      border: none;
      outline: none;
      background: rgba(255, 255, 255, 0.05);

      &:hover {
        background: rgba(255, 255, 255, 0.07);
      }
    }
  }

  .search-result {
    width: 100%;
    display: flex;
    flex-flow: column;

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
      grid-template-columns: conv(190) conv(60) conv(26) conv(95) conv(117) auto;
    }

    &__body {
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
      grid-template-columns: conv(190) conv(60) conv(26) conv(95) conv(117) auto auto;

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

      .licenses {
        min-height: conv(78);
        padding: conv(6) 0;
        width: calc(100% + 1rem);
        padding-right: 0.2rem;
      }

      /* .btn-wanted,
      .btn-remove-wanted {
        border-radius: 0.4rem;
        padding: 0.6rem;
        font-size: 1rem;
        width: fit-content;
        white-space: pre;
        &:hover {
          transform: scale(1.1);
        }
        &:active {
          transform: scale(1.05);
        }
      }
      .btn-wanted {
        background: #ffd130;
        color: #000;
        font-weight: bold;
        &.fbi {
          background: #fb7712;
          color: #fff;
        }
        &.army {
          background: #6c8073;
          color: #fff;
        }
      }
      .btn-remove-wanted {
        background: #333333;
        color: #ffd130;
        &.fbi {
          color: #fb7712;
        }
        &.army {
          color: #8da997;
        }
      } */
    }
  }
}

/* MDT visual restyle */
.title {
  margin-bottom: conv(18) !important;
  min-height: conv(34) !important;

  .text,
  .text span:first-child {
    font-size: conv(22) !important;
    line-height: conv(26) !important;
    letter-spacing: 0.04em !important;
    color: #fff !important;
  }

  .btn,
  .text span:last-child {
    height: conv(30) !important;
    min-width: conv(96) !important;
    border-radius: conv(7) !important;
    border: 1px solid rgba(255, 255, 255, 0.08) !important;
    background: rgba(255, 255, 255, 0.045) !important;
    font-size: conv(12) !important;
    letter-spacing: 0.06em !important;
    color: rgba(255, 255, 255, 0.72) !important;
    overflow: hidden !important;

    &::after {
      background: linear-gradient(135deg, rgba(31, 139, 255, 0.72), rgba(40, 180, 255, 0.26)) !important;
    }

    &.active,
    &:hover {
      color: #fff !important;
      border-color: rgba(31, 139, 255, 0.38) !important;
      box-shadow: 0 0 1rem rgba(31, 139, 255, 0.14) !important;
    }
  }
}

.search-wrap {
  height: conv(54) !important;
  min-height: conv(54) !important;
  margin-bottom: conv(24) !important;

  input {
    border: 1px solid rgba(255, 255, 255, 0.085) !important;
    border-radius: conv(10) !important;
    background: rgba(255, 255, 255, 0.035) !important;
    padding: 0 conv(18) !important;
    font-size: conv(15) !important;
    letter-spacing: 0.04em !important;

    &:focus {
      border-color: rgba(31, 139, 255, 0.45) !important;
      box-shadow: 0 0 1.1rem rgba(31, 139, 255, 0.12) !important;
    }
  }

  .btn-find {
    width: conv(126) !important;
    min-width: conv(126) !important;
    border-radius: conv(10) !important;
    border: 1px solid rgba(31, 139, 255, 0.38) !important;
    background: linear-gradient(135deg, rgba(31, 139, 255, 0.88), rgba(37, 190, 255, 0.58)) !important;
    font-weight: 900 !important;
    color: #fff !important;
    box-shadow: 0 0 1.4rem rgba(31, 139, 255, 0.18) !important;

    &:hover {
      filter: brightness(1.12) !important;
      transform: translateY(-1px) !important;
    }
  }
}

.search-result {
  gap: conv(10) !important;

  &__head {
    padding: 0 conv(16) !important;
    margin-bottom: conv(8) !important;
    font-size: conv(12) !important;
    line-height: conv(15) !important;
    letter-spacing: 0.08em !important;
    color: rgba(255, 255, 255, 0.45) !important;
  }

  &__body {
    min-height: conv(74) !important;
    border: 1px solid rgba(255, 255, 255, 0.075) !important;
    border-radius: conv(12) !important;
    background: rgba(255, 255, 255, 0.035) !important;
    padding-left: conv(16) !important;
    font-size: conv(16) !important;
    line-height: conv(19) !important;
    letter-spacing: 0.025em !important;
    box-shadow: inset 0 0 0 1px rgba(255, 255, 255, 0.015) !important;

    .btn-wanted,
    .btn-remove-wanted {
      height: conv(44) !important;
      width: conv(112) !important;
      min-width: conv(112) !important;
      margin-right: conv(12) !important;
      border-radius: conv(9) !important;
      background: rgba(31, 139, 255, 0.12) !important;
      border: 1px solid rgba(31, 139, 255, 0.25) !important;
      font-size: conv(13) !important;
      letter-spacing: 0.055em !important;

      &::after {
        background: linear-gradient(135deg, rgba(31, 139, 255, 0.7), rgba(40, 180, 255, 0.24)) !important;
      }
    }
  }
}

/* Premium MDT result/card pass */
.find-human-page,
.find-car-page {
  .title {
    margin-bottom: conv(12) !important;
    padding-bottom: conv(10) !important;
    border-bottom: 1px solid rgba(94, 185, 255, 0.09) !important;

    .text,
    .text span:first-child {
      font-size: conv(24) !important;
      line-height: conv(27) !important;
      font-weight: 900 !important;
      letter-spacing: 0.075em !important;
      text-shadow: 0 0 conv(15) rgba(65, 174, 255, 0.22) !important;
    }

    .btn,
    .text span:last-child {
      height: conv(32) !important;
      min-width: conv(112) !important;
      padding: 0 conv(15) !important;
      border-radius: conv(8) !important;
      background: rgba(6, 19, 36, 0.62) !important;
      border-color: rgba(106, 193, 255, 0.13) !important;
      box-shadow: inset 0 0 conv(14) rgba(31, 139, 255, 0.035) !important;
    }
  }

  .search-wrap {
    height: conv(50) !important;
    min-height: conv(50) !important;
    margin-bottom: conv(14) !important;

    input {
      background: rgba(3, 12, 25, 0.58) !important;
      border-color: rgba(103, 190, 255, 0.14) !important;
      border-radius: conv(9) !important;
      box-shadow: inset 0 0 conv(20) rgba(0, 0, 0, 0.18) !important;
    }

    .btn-find {
      width: conv(116) !important;
      min-width: conv(116) !important;
      border-radius: conv(9) !important;
      background: linear-gradient(135deg, #1f8bff, rgba(34, 212, 255, 0.72)) !important;
      box-shadow: 0 0 conv(20) rgba(31, 139, 255, 0.22), inset 0 0 conv(14) rgba(255, 255, 255, 0.1) !important;
    }
  }

  .search-result {
    flex: 1 !important;
    min-height: 0 !important;

    &__head {
      padding: 0 conv(14) !important;
      margin-bottom: conv(7) !important;
      color: rgba(155, 212, 255, 0.58) !important;
    }

    &__body {
      position: relative !important;
      min-height: conv(88) !important;
      border-radius: conv(12) !important;
      background:
        radial-gradient(circle at 0 0, rgba(31, 139, 255, 0.14), transparent 42%),
        linear-gradient(135deg, rgba(255, 255, 255, 0.055), rgba(255, 255, 255, 0.018)),
        rgba(3, 12, 25, 0.66) !important;
      border-color: rgba(104, 191, 255, 0.16) !important;
      box-shadow: inset 0 0 0 1px rgba(255, 255, 255, 0.018), 0 0 conv(24) rgba(0, 0, 0, 0.18) !important;
      overflow: hidden !important;

      &::before {
        content: "VERIFIED RECORD";
        position: absolute;
        top: conv(7);
        right: conv(12);
        font-size: conv(8);
        line-height: conv(10);
        font-weight: 900;
        letter-spacing: 0.16em;
        color: rgba(95, 255, 184, 0.76);
        text-shadow: 0 0 conv(10) rgba(95, 255, 184, 0.35);
      }

      &::after {
        content: "";
        position: absolute;
        left: 0;
        top: 12%;
        width: conv(4);
        height: 76%;
        border-radius: 999px;
        background: #4db5ff;
        box-shadow: 0 0 conv(16) rgba(77, 181, 255, 0.62);
      }

      .wanted .star svg path[fill="#FFDF6D"] {
        filter: drop-shadow(0 0 conv(7) rgba(255, 216, 107, 0.48));
      }

      .licenses {
        color: rgba(203, 231, 255, 0.76) !important;
        font-size: conv(13) !important;
      }

      .btn-wanted,
      .btn-remove-wanted {
        height: conv(40) !important;
        width: conv(104) !important;
        min-width: conv(104) !important;
        background: rgba(255, 216, 107, 0.1) !important;
        border-color: rgba(255, 216, 107, 0.28) !important;
        color: #ffe58a !important;
        box-shadow: 0 0 conv(16) rgba(255, 216, 107, 0.11) !important;
      }
    }
  }
}

/* Asset-backed MDT dossier/profile pass */
.record-dossier {
  min-height: conv(96);
  margin-bottom: conv(12);
  padding: conv(12) conv(14);
  display: grid;
  grid-template-columns: conv(68) 1fr conv(104);
  gap: conv(14);
  align-items: center;
  position: relative;
  overflow: hidden;
  border-radius: conv(13);
  border: 1px solid rgba(96, 194, 255, 0.2);
  background:
    radial-gradient(circle at 0 0, rgba(31, 139, 255, 0.18), transparent 36%),
    linear-gradient(135deg, rgba(255, 255, 255, 0.058), rgba(255, 255, 255, 0.018)),
    rgba(2, 11, 25, 0.76);
  box-shadow: inset 0 0 conv(30) rgba(35, 139, 255, 0.08), 0 0 conv(22) rgba(0, 0, 0, 0.2);

  &::before {
    content: "";
    pointer-events: none;
    position: absolute;
    inset: 0;
    opacity: 0.16;
    background-image:
      linear-gradient(rgba(91, 190, 255, 0.08) 1px, transparent 1px),
      linear-gradient(90deg, rgba(91, 190, 255, 0.06) 1px, transparent 1px);
    background-size: conv(20) conv(20);
  }

  .record-avatar {
    width: conv(68);
    height: conv(68);
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: conv(12);
    border: 1px solid rgba(119, 211, 255, 0.24);
    background: rgba(255, 255, 255, 0.04);
    box-shadow: inset 0 0 conv(16) rgba(31, 139, 255, 0.1);

    img {
      width: conv(52);
      height: conv(52);
      object-fit: contain;
      filter: drop-shadow(0 0 conv(12) rgba(93, 199, 255, 0.45));
    }
  }

  .record-summary {
    display: flex;
    flex-direction: column;
    min-width: 0;
    text-transform: uppercase;

    .record-kicker {
      margin-bottom: conv(5);
      font-size: conv(10);
      line-height: conv(12);
      font-weight: 900;
      letter-spacing: 0.18em;
      color: rgba(118, 207, 255, 0.74);
    }

    strong {
      font-size: conv(24);
      line-height: conv(27);
      letter-spacing: 0.055em;
      color: #fff;
      text-shadow: 0 0 conv(16) rgba(91, 190, 255, 0.24);
    }

    small {
      margin-top: conv(4);
      font-size: conv(12);
      line-height: conv(14);
      font-weight: 800;
      letter-spacing: 0.07em;
      color: rgba(255, 255, 255, 0.54);
    }
  }

  .record-status {
    height: conv(36);
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: conv(8);
    border: 1px solid rgba(95, 255, 184, 0.2);
    background: rgba(95, 255, 184, 0.08);
    font-weight: 900;
    font-size: conv(12);
    line-height: conv(14);
    letter-spacing: 0.13em;
    color: rgba(139, 255, 198, 0.95);
    box-shadow: 0 0 conv(16) rgba(95, 255, 184, 0.1);

    &.danger {
      border-color: rgba(255, 75, 91, 0.28);
      background: rgba(255, 75, 91, 0.1);
      color: #ff9ca8;
      box-shadow: 0 0 conv(18) rgba(255, 75, 91, 0.14);
    }
  }
}

.search-result__body {
  min-height: conv(78) !important;
}

/* PDF-layout correction pass: large search/profile panels */
.find-human-page,
.find-car-page {
  .title {
    justify-content: center !important;
    min-height: conv(62) !important;
    margin-bottom: conv(22) !important;
    border-bottom: 0 !important;

    .text,
    .text span:first-child {
      font-size: conv(34) !important;
      line-height: conv(38) !important;
      text-align: center !important;
      letter-spacing: 0.1em !important;
    }

    .btn,
    .text span:last-child {
      margin-left: conv(16) !important;
      height: conv(38) !important;
      background: linear-gradient(90deg, rgba(103, 57, 222, 0.7), rgba(31, 121, 225, 0.56)) !important;
    }
  }

  .search-wrap {
    max-width: conv(760) !important;
    height: conv(70) !important;
    min-height: conv(70) !important;
    margin: 0 auto conv(24) !important;
    padding: conv(7) !important;
    border-radius: conv(4) !important;
    border: 1px solid rgba(118, 204, 255, 0.22) !important;
    background: rgba(0, 0, 0, 0.48) !important;
    box-shadow: inset 0 0 conv(26) rgba(0, 0, 0, 0.44), 0 0 conv(28) rgba(56, 150, 255, 0.12) !important;

    input {
      border: 0 !important;
      border-radius: conv(2) !important;
      background: rgba(0, 0, 0, 0.42) !important;
      font-size: conv(20) !important;
      line-height: conv(24) !important;
      text-align: center !important;
      letter-spacing: 0.09em !important;
    }

    .btn-find {
      height: 100% !important;
      width: conv(148) !important;
      min-width: conv(148) !important;
      border-radius: conv(3) !important;
      background: linear-gradient(90deg, #6b42ee, #257dff) !important;
      font-size: conv(17) !important;
      letter-spacing: 0.12em !important;
    }
  }

  .record-dossier {
    grid-template-columns: conv(150) 1fr conv(132) !important;
    min-height: conv(152) !important;
    margin-bottom: conv(18) !important;
    background: linear-gradient(135deg, rgba(5, 14, 32, 0.84), rgba(22, 37, 78, 0.48)) !important;

    .record-avatar {
      width: conv(128) !important;
      height: conv(128) !important;

      img {
        width: conv(92) !important;
        height: conv(92) !important;
      }
    }

    .record-summary strong {
      font-size: conv(36) !important;
      line-height: conv(39) !important;
    }

    .record-status {
      height: conv(48) !important;
      font-size: conv(15) !important;
    }
  }

  .search-result__body {
    min-height: conv(94) !important;
    background: linear-gradient(135deg, rgba(2, 9, 24, 0.8), rgba(16, 27, 58, 0.52)) !important;
  }
}
</style>