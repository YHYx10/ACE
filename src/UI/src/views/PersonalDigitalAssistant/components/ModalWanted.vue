<template>
  <div class="modal-wrap">
    <div class="modal-wanted">
      <button class="field__close" @click="closeModal">
        <div>
          <img src="/img/inputMenu/arrow.svg" alt="" />
        </div>
      Back
      </button>
      <div class="title">{{ loc("Pda_30") }}</div>
      <div class="stars-list">
        <div
          :class="[{ active: item <= currentWantedLevel }, 'star']"
          v-for="(item, index) in 5"
          :key="item.key"
          @click="setCurrentWantedLevel(index + 1)"
        >
          <svg
            v-if="item <= currentWantedLevel"
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
      <textarea
        class="textarea"
        rows="3"
        :placeholder="loc('pda:wanted:plh')"
        v-model="reason"
      ></textarea>
      <div class="btns-wrap">
        <div
          :class="[
            { army: type === 2 },
            { fbi: type === 1 },
            'btn',
            'btn-edit',
            'active',
            'bank-btn',
          ]"
          v-if="wantedLevel !== currentWantedLevel && reason"
          @click="setWanted"
        >
          {{ loc("Pda_36") }}
        </div>
        <div class="btn btn-edit" v-else>{{ loc("Pda_36") }}</div>
        <div class="btn active" v-if="reason" @click="removeWanted">
          {{ loc("Pda_35") }}
        </div>
        <div class="btn" v-else>{{ loc("Pda_35") }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
export default {
  name: "ModalWanted",

  props: {
    typeModal: String,
    id: Number,
    wantedLevel: Number,
    reason: String,
  },

  data: function () {
    return {
      currentWantedLevel: null,
      currentReason: null,
    };
  },

  computed: {
    ...mapState("personalDigitalAssistant", ["type"]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    closeModal: function () {
      this.$emit("closeModal");
    },
    setCurrentWantedLevel: function (index) {
      this.currentWantedLevel = index;
    },
    setWanted: function () {
      window.mp.trigger(
        "pda:setWanted",
        this.typeModal,
        this.id,
        this.currentWantedLevel,
        this.reason
      );
      this.closeModal();
    },
    removeWanted: function () {
      window.mp.trigger(
        "pda:setWanted",
        this.typeModal,
        this.id,
        0,
        this.reason
      );
      this.closeModal();
    },
  },

  created() {
    this.setCurrentWantedLevel(this.wantedLevel);
    this.currentReason = this.reason;
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.modal-wanted {
  display: flex;
  flex-flow: column;
  align-items: center;
  justify-content: center;
  background: linear-gradient(
    180deg,
    rgba(14, 14, 14, 0.95) 0%,
    rgba(14, 14, 15, 0.95) 100%
  );
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  padding: conv(44) conv(97) conv(44);

  .field__close {
    display: flex;
    align-items: center;
    background: none;
    outline: none;
    border: none;
    font-weight: 700;
    font-size: 1.053rem;
    line-height: 1.263rem;
    color: #ffffff;
    cursor: pointer;

    div {
      width: 2.474rem;
      height: 2.474rem;
      display: flex;
      align-items: center;
      justify-content: center;
      margin-right: 0.737rem;
      background: rgba(255, 255, 255, 0.07);

      img {
        height: 0.737rem;
        width: auto;
      }
    }
  }

  .title {
    font-weight: 700;
    font-size: conv(24);
    line-height: conv(29);
    display: flex;
    align-items: center;
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    margin: conv(41) 0 conv(22);
  }

  .stars-list {
    display: flex;
    align-items: center;

    .star {
      height: conv(34);

      &:not(:last-child) {
        margin-right: conv(3.5);
      }

      svg{
        height: 100%;
        width: auto;
      }
    }
  }

  .textarea {
    margin-top: conv(38.57);
    width: conv(310);
    height: conv(126);
    resize: none;
    margin-bottom: conv(20);
    background: rgba(217, 217, 217, 0.05);
    border: none;
    outline: none;
    padding: conv(20);
    color: white;

    &,
    &::placeholder {
      font-weight: 700;
      font-size: conv(14);
      line-height: conv(17);
      display: flex;
      align-items: center;
      text-transform: uppercase;
    }

    &::placeholder {
      color: rgba(255, 255, 255, 0.5);
    }
    box-sizing: border-box;
  }

  .btns-wrap {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    width: conv(310);

    .btn {
      width: 100%;
      height: conv(75);
      transition: 0.2s ease;
      display: flex;
      align-items: center;
      justify-content: center;
      font-weight: 700;
      font-size: conv(24);
      line-height: conv(29);
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;

      &:not(:last-child) {
        margin-bottom: conv(10);
      }

      &.active {
        cursor: pointer;

        &:not(.btn-edit) {
          &:hover {
            background: rgba(255, 255, 255, 0.15);
          }
        }
      }

      &.btn-edit {
        background: linear-gradient(180deg, #301934  0%, #591b87 100%);
      }

      &:not(.btn-edit) {
        background: rgba(255, 255, 255, 0.05);
      }

      &:not(.active) {
        pointer-events: none;
        opacity: 0.2;
      }
    }
  }
}
</style>
