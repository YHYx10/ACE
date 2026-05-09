<template>
  <div class="input-menu">
    <div class="input-menu-wrap">
      <div class="input-menu__field">
        <button class="field__close" @click="cancel">
          <div>
            <img src="/img/inputMenu/arrow.svg" alt="" />
          </div>
          {{ loc('mmain:frac:dialog:cancel') }}
        </button>
        <span class="field__title">{{ loc(dialog.tittle) }}</span>
        <span class="field__subtitle">{{ loc(dialog.subtittle) }}</span>
        <input
          v-if="dialog.input"
          :type="dialog.input"
          class="field__action"
          v-model="dialog.value"
          :placeholder="loc(dialog.placeholder)"
        />
        <input
          v-if="dialog.input2"
          :type="dialog.input2"
          class="field__action"
          v-model="dialog.value2"
          style="margin-top: 0.5rem"
          :placeholder="loc(dialog.placeholder2)"
        />
        <div v-if="dialog.keyHandler" class="field__key">
          {{ dialog.value }}
        </div>
      </div>
      <div class="input-menu__buttons">
        <ModalBtn
          class="primary"
          @click="confirm(dialog.callback, dialog.value, dialog.value2)"
          :button="{ text: 'mmain:frac:dialog:success' }"
        />
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations } from 'vuex'
import ModalBtn from '../../UI/button/ModalBtn.vue'
export default {
  props: ['dialog'],
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  components: {
    ModalBtn,
  },
  methods: {
    cancel() {
      this.setDialog(null)
    },
    confirm(callback, value, value2) {
      if (typeof callback == 'function') callback(value, value2)
      this.setDialog(null)
    },
    onKeyPress(e) {
      this.dialog.value = e.keyCode
    },
    ...mapMutations('optionsMenu', ['setDialog']),
  },
  mounted() {
    if (this.dialog && this.dialog.keyHandler)
      document.addEventListener('keyup', this.onKeyPress)
  },
  beforeDestroy() {
    document.removeEventListener('keyup', this.onKeyPress)
  },
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
  z-index: 9999;

  &,
  div,
  span,
  button {
    font-family: 'Akrobat';
  }

  &-wrap {
    display: flex;
    align-items: center;
    flex-direction: column;
    padding: 2.579rem 3.526rem;
    position: relative;
    background: rgba(0, 0, 0, 0.95);

    &::after {
      content: '';
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

    &__subtitle {
      font-weight: 700;
      font-size: 1rem;
      line-height: 2.526rem;
      text-align: center;
      text-transform: uppercase;
      color: #6e6e6e;
      margin-bottom: 1rem;
    }

    &__action {
      width: 16.316rem;
      height: 3.421rem;
      background: rgba(217, 217, 217, 0.05);
      text-align: center;
      border: none;
      color: white;
      &::-webkit-outer-spin-button,
      &::-webkit-inner-spin-button {
        -webkit-appearance: none;
        margin: 0;
      }

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

    &__key {
      width: 19rem;
      height: 3.5rem;
      margin: 0 auto;
      display: flex;
      font-size: 1.5rem;
      letter-spacing: 0.04rem;
      align-items: center;
      justify-content: center;
      background: rgba(255, 255, 255, 0.07);
      color: #fff;
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
