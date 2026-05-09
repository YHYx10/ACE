<template>
  <div class="modal-wrap-modify">
    <div class="row">
      <Back @click="closeModal" />
    </div>

    <div class="window-slide">
      <transition name="modal-slide">
        <div class="content" v-if="show">
          <div class="comment">
        The fight for"{{ dataBuffer[currentWar].bizName }}"
          </div>
          <battle-row :data="dataBuffer[currentWar]" />
        </div>
      </transition>
    </div>

    <div class="btns-wrap">
      <div class="manage-slide" v-if="dataBuffer.length > 1">
        <div
          :style="{
            transform: clickedSwitcher == 1 ? 'scale(1.2)' : 'scale(1)',
          }"
          @click="showPrevhar"
          @mouseover="hoveredArrow = 1"
          @mouseleave="hoveredArrow = 0"
          class="btn-arrow"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="15"
            height="18"
            fill="none"
            viewBox="0 0 15 18"
          >
            <path fill="#fff" :d="svgArrow" />
          </svg>
        </div>
   Choose the action
        <div
          
          :style="{
            transform: clickedSwitcher == 2 ? 'scale(1.2)' : 'scale(1)',
          }"
          @click="showNextWar"
          @mouseover="hoveredArrow = 2"
          @mouseleave="hoveredArrow = 0"
          class="btn-arrow"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="15"
            height="18"
            fill="none"
            viewBox="0 0 15 18"
            style="transform: rotate(-180deg);"
          >
            <path fill="#fff" :d="svgArrow" />
          </svg>
        </div>
      </div>
      <div class="btns">
        <DefaultBtn class="" @click="toNextWar(true)">{{
          loc('fam:btl:notify:succ')
        }}</DefaultBtn>
        <DefaultBtn secondary class="" @click="toNextWar(false)">{{
          loc('fam:btl:notify:cncl')
        }}</DefaultBtn>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import BattleRow from './BattleRow.vue'
import Back from '../../components/Back'
import DefaultBtn from '../../../UI/button/DefaultBtn.vue'

export default {
  name: 'ModalNotify',

  props: {
    data: Array,
  },

  components: {
    BattleRow,
    Back,
    DefaultBtn,
  },

  data() {
    return {
      show: false,
      dataBuffer: [],
      hoveredArrow: 0,
      currentWar: 0,
      clickedSwitcher: 0,
      svgArrow:
        'm1.496 10.664 9.543 6.362c1.684 1.123 3.78-.68 2.92-2.513l-2.186-4.664a2 2 0 0 1 0-1.698l2.186-4.664c.86-1.833-1.236-3.636-2.92-2.513L1.496 7.336a2 2 0 0 0 0 3.328Z',
    }
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    closeModal() {
      this.$emit('closeModalNotify')
    },
    showNextWar() {
      this.clickedSwitcher = 2
      setTimeout(() => {
        this.clickedSwitcher = 0
      }, 100)
      this.show = false
      setTimeout(() => {
        this.show = true
      }, 300)

      if (this.currentWar + 1 == this.dataBuffer.length) {
        this.currentWar = 0
      } else this.currentWar++
    },
    showPrevhar() {
      this.clickedSwitcher = 1
      setTimeout(() => {
        this.clickedSwitcher = 0
      }, 100)
      this.show = false
      setTimeout(() => {
        this.show = true
      }, 300)

      if (this.currentWar == 0) {
        this.currentWar = this.dataBuffer.length - 1
      } else this.currentWar--
    },
    toNextWar(accepted) {
      console.log(JSON.stringify(this.data))
      window.mp.trigger(
        'familyMenu:acceptBattle',
        this.dataBuffer[this.currentWar].id,
        accepted
      )
      if (this.dataBuffer.length == 1) {
        this.show = false
        this.dataBuffer.shift()

        setTimeout(() => {
          this.closeModal()
        }, 300)
      } else {
        this.show = false
        this.dataBuffer.splice(this.currentWar, 1)
        this.currentWar = 0
        setTimeout(() => {
          this.show = true
        }, 300)
      }
    },
  },

  mounted() {
    this.show = true
    this.dataBuffer = [...this.data]
  },
}
</script>

<style lang="scss" scoped>
div,
span,
button {
  font-family: Akrobat;
}

.modal-slide-enter-active {
  transition: all 0.3s ease;
}
.modal-slide-leave-active {
  transition: all 0.3s ease;
}
.modal-slide-enter {
  transform: translateX(70rem);
  opacity: 0;
}
.modal-slide-leave-to {
  transform: translateX(-70rem);
  opacity: 0;
}
.modal-wrap-modify {
  position: fixed;
  z-index: 99;
  top: 0;
  left: 0;
  height: 100vh;
  width: 100vw;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  background: rgba(0, 0, 0, 0.97);
  .row {
    display: flex;
    justify-content: center;
  }
  .btns-wrap {
    margin-top: 2.963vh;
    width: 56.481vh;
    display: flex;
    flex-direction: column;
    gap: 1.019vh;
    .manage-slide {
      display: flex;
      align-items: center;
      justify-content: space-between;
      font-weight: 700;
      font-size: 2.222vh;
      line-height: 2.778vh;
      .btn-arrow {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 6.111vh;
        height: 6.111vh;
        background: rgba(255, 255, 255, 0.1);
        svg {
          width: 1.389vh;
          height: 1.667vh;
        }
        &:hover {
          background: rgba(255, 255, 255, 0.14);
        }
      }
    }
    .btns-wrap-text {
      display: flex;
      justify-content: center;
      margin-bottom: 2rem;
      text-transform: uppercase;
      font-style: normal;
      font-weight: bold;
      font-size: 2rem;
    }

    .btns {
      display: flex;
      gap: 1.852vh;
      button {
        width: 27.315vh;
        height: 6.944vh;
        font-weight: 700;
        font-size: 2.222vh;
        line-height: 2.778vh;
        text-transform: uppercase;
        color: #ffffff;
      }
    }
  }
}

.window-slide {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
  position: relative;
  .content {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    margin-top: 2.407vh;
  }
  .comment {
    width: 50%;
    margin-bottom: 2.685vh;
    text-transform: uppercase;
    font-weight: bold;
    font-size: 1.7rem;
    text-align: center;
    color: #ffffff;
  }
}
</style>
