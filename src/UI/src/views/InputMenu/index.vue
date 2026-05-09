<template>
  <div class="input-menu">
    <div class="input-menu-wrap">
      <div class="input-menu__field">
        <button class="field__close" @click="close">
          <div>
            <img src="/img/inputMenu/arrow.svg" alt="" />
          </div>
          {{ loc("dynamic_input_2") }}
        </button>
        <span class="field__title">{{ loc(title) }}</span>
        <input
          v-model="input"
          type="text"
          class="field__action"
          :length="length"
          :placeholder="loc(plHolder)"
        />
      </div>
      <div class="input-menu__buttons">
        <ModalBtn
          class="primary"
          @click="send"
          :button="{ text: 'dynamic_input_1' }"
        />
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex"
import ModalBtn from "../UI/button/ModalBtn.vue"

export default {
  name: "InputMenu",
  data: function() {
    return {
      input: "",
    }
  },
  computed: {
    ...mapState("inputMenu", ["title", "plHolder", "length"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    send: function() {
      if (this.input == "") return
      window.mp.trigger("input", this.input)
      this.input = ""
    },
    close() {
      window.mp.trigger("closeInput")
    },
  },
  mounted() {
    addEventListener("mousemove", this.onMouseMove)
  },
  beforeDestroy() {
    removeEventListener("mousemove", this.onMouseMove)
  },
  components: { ModalBtn },
}
</script>

<style lang="scss" scoped>
.input-menu {
  width: 100%;
  height: 100%;
  position: absolute;
  display: flex;
  align-items: center;
  justify-content: center;
  top: 0;
  left: 0;

  &,
  div,
  span,
  button {
    font-family: "Akrobat";
  }

  &-wrap {
    display: flex;
    align-items: center;
    flex-direction: column;
    padding: 2.579rem 3.526rem;
    position: relative;
    background: rgba(0, 0, 0, 0.95);

    &::after {
      content: "";
      position: absolute;
      width: 24.1053rem;
      height: 24.1053rem;
      left: 50%;
      bottom: 0;
      transform: translate(-50%, 0);
      background: rgba(255, 255, 255, 0.3);
      opacity: 0.25;
      filter: blur(111px);
      border-radius: 50%;
      z-index: 1;
    }

    div {
      z-index: 2;
    }
  }

  &__field {
    display: flex;
    flex-direction: column;
    align-items: center;

    span {
      display: block;
      text-transform: uppercase;
      color: #ffffff;
      text-align: center;
    }
  }

  .field {
    &__close {
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
      margin-bottom: 1.579rem;
      &:hover {
        div {
          background: rgba(255, 255, 255, 0.15);
        }
      }

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

    &__title {
      font-weight: 700;
      font-size: 2.105rem;
      line-height: 2.526rem;
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;
      margin-bottom: 1.526rem;
    }

    &__action {
      width: 16.316rem;
      height: 3.421rem;
      background: rgba(217, 217, 217, 0.05);
      text-align: center;
      border: none;
      color: white;

      &,
      &::placeholder {
        font-weight: 700;
        font-size: 0.737rem;
        line-height: 0.895rem;
        text-transform: uppercase;
      }

      &::placeholder {
        color: rgba(255, 255, 255, 0.5);
      }
    }
  }

  &__buttons {
    width: 100%;
    margin-top: 1.053rem;

    div {
      cursor: pointer;
      display: flex;
      align-items: center;
      justify-content: center;
      width: 100%;
      height: 3.947rem;
      font-weight: 700;
      font-size: 1.263rem;
      line-height: 1.526rem;
      text-transform: uppercase;
      color: #ffffff;
      transition: 0.5s ease;
      border: 1px solid transparent;
      background: rgba(255, 255, 255, 0.05);

      &:hover {
        background: linear-gradient(
          180deg,
          rgba(71, 44, 132, 0.25) 0%,
          rgba(75, 0, 130, 0.25) 100%
        );
        border: 1px solid #301934 ;
        box-shadow: inset 0px 0px 0.789rem rgba(75, 0, 130, 0.86);
      }
    }
  }
}
</style>
