<template>
  <transition name="scale-in">
    <div class="window">
      <div class="dialog-menu">
        <span class="dialog-menu__header">{{ loc('familyMenu_39') }}</span>
        <div :class="[{ column: true }, 'dialog-menu__actions']">
          <ModalInput type="number" v-model.number="money" />
          <ModalBtn
            v-if="type === 0"
            @click="takeMoney"
            class="primary"
            :button="{
              text: 'familyMenu_40',
              icon: null,
              index: 1,
              isColumn: true,
            }"
          />
          <ModalBtn
            v-else
            @click="putMoney"
            class="primary"
            :button="{
              text: 'familyMenu_41',
              icon: null,
              index: 2,
              isColumn: true,
            }"
          />
          <ModalBtn
            @click="closeModal"
            :button="{
              text: 'familyMenu_36',
              icon: null,
              index: 3,
              isColumn: true,
            }"
          />
        </div>
      </div>
    </div>
  </transition>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import ModalBtn from '../../UI/button/ModalBtn.vue'
import ModalInput from '../components/ModalInput.vue'
export default {
  name: 'ModalMoney',
  props: {
    type: Number,
  },
  computed: {
    ...mapState('familyMenu', ['infoPage']),
    ...mapGetters('localization', ['loc']),
  },
  data: function() {
    return {
      money: null,
    }
  },
  methods: {
    closeModal: function() {
      this.$emit('closeModalMoney', null)
    },
    takeMoney: function() {
      if (this.money) {
        window.mp.trigger('familyMenu:takeMoney', this.money)
        this.closeModal()
      }
    },
    putMoney: function() {
      if (this.money) {
        window.mp.trigger('familyMenu:putMoney', this.money)
        this.closeModal()
      }
    },
  },
  components: { ModalInput, ModalBtn },
}
</script>

<style lang="scss" scoped>
@import '../modal.scss';
// .window {
//   position: absolute;
//   top: 0;
//   left: 0;
//   width: 100vw;
//   height: 100vh;
//   z-index: 98;
// }

// .dialog-menu {
//   position: absolute;
//   top: 50%;
//   left: 50%;
//   transform: translate(-50%, -50%);
//   z-index: 99;
//   width: 23.368rem;
//   padding: 2.211rem 3.526rem 2.579rem;
//   font-family: "Akrobat";
//   background: rgba(0, 0, 0, 0.95);

//   span {
//     width: 100%;
//     display: block;
//     margin-bottom: 1.684rem;
//     font-weight: 700;
//     font-size: 1.263rem;
//     line-height: 1.526rem;
//     text-align: center;
//     text-transform: uppercase;
//     color: #ffffff;
//     white-space: pre-wrap;
//     position: relative;
//   }

//   div {
//     display: grid;
//     grid-template-rows: 1fr 1fr;
//     row-gap: 0.526rem;
//     width: 100%;
//   }
// }
// .scale-in-enter-active,
// .scale-in-leave-active {
//   transition: all 0.4s;
// }
// .scale-in-enter,
// .scale-in-leave-to {
//   transform: scale(0.2);
//   opacity: 0;
// }
</style>
