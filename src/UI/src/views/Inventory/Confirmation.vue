<template>
    <!-- <div class="confirm">
        <div class="confirm-dialog">
            <div class="confirm-tittle">
                {{loc("inv_confirm_tittle")}}
            </div>
            <div class="confirm-body">
                {{loc(msg)}}
            </div>
            <div class="confirm-buttons">
                    <div class="confirm-button" @click="confirm">{{loc("inv_split_confirm")}}</div>
                    <div class="confirm-button" @click="cancel">{{loc("inv_split_cancel")}}</div>
            </div>
        </div>
    </div> -->
    <div class="input-menu">
        <div class="input-menu-wrap">
            <div class="input-menu__field">
                <span class="field__title">{{ loc("inv_confirm_tittle") }}</span>
                <span class="field__subtitle">{{ loc(msg) }}</span>
            </div>
            <div class="input-menu__buttons">
                <ModalBtn
                    class="primary"
                    @click="confirm"
                    :button="{ text: 'inv_split_confirm' }"
                />
                <ModalBtn
                    class=""
                    @click="cancel"
                    :button="{ text: 'inv_split_cancel' }"
                />
            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
import ModalBtn from '../UI/button/ModalBtn.vue';

export default {
    props: ["msg"],
    computed: {
        ...mapGetters("localization", ["loc"])
    },
    methods: {
        confirm() {
            this.$emit("confirmAction", true);
        },
        cancel() {
            this.$emit("confirmAction", false);
        }
    },
    components: { ModalBtn }
}
</script>

<style lang="scss" scoped>
.input-menu {
  width: 100%;
  height: 100%;
  position: fixed;
  display: flex;
  align-items: center;
  justify-content: center;
  top: 0;
  left: 0;
  z-index: 1100;

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
      max-width: 28vw;
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
    display: flex;
    gap: 0.5rem;
  }
}
    // .confirm{
    //     position: fixed;
    //     left: 0;
    //     top: 0;
    //     width: 100vw;
    //     height: 100vh;
    //     z-index: 1100;
    //     background-color: rgba(0,0,0,.5);
    //     display: flex;
    //     justify-content: center;
    //     align-items: center;
    //     &-dialog{
    //         width: 19vh;
    //         border: 2px solid rgba(#fff, .2);
    //         box-shadow: 0px 1rem 3rem rgba(0, 13, 18, 0.5);
    //     }
    //     &-tittle{
    //         height: 4vh; 
    //         background: url(/img/inventory/common/otherHead.png) no-repeat center;
    //         background-size: cover;          
    //         font-size: 1.3vh;
    //         line-height: 1.3vh;
    //         color: #fff;
    //         display: flex;
    //         align-items: center;
    //         justify-content: center;
    //     }
    //     &-body{
    //         color: #fff;
    //         padding: 2rem 1.2vh;
    //         font-size: .9vh;
    //         line-height: 1.2vh;
    //         letter-spacing: .05vh;
    //         background-color: #142126;
    //         text-align: center;
    //     }
    //     &-buttons{
    //         display: flex;
    //         align-items: center;
    //         justify-content: space-around;
    //     }
    //     &-button{
    //         height: 3.5vh;
    //         width: 49%;
    //         color: rgba(#fff, .5);
    //         background-color: #000D12;
    //         display: flex;
    //         justify-content: center;
    //         align-items: center;
    //         &:hover{
    //             background-color: #8E52AA;
    //             color: #fff;
    //         }
    //     }
    // }
</style>