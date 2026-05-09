<template>
  <transition name="scale-in">
    <div class="dialog-menu">
      <span class="dialog-menu__header">{{ loc(Body.Header) }}</span>
      <div :class="[{ column: isColumnClass }, 'dialog-menu__actions']">
        <ModalBtn
          v-for="(button, index) in Body.Buttons"
          :key="index"
          :button="{
            text: button.Name,
            icon: button.Icon,
            index,
            isColumn: isColumnClass,
          }"
          :class="{primary: index !== Body.Buttons.length - 1}"
          @click="onClick(index)"
        />
      </div>
    </div>
  </transition>
</template>

<script>
import { mapState, mapGetters } from "vuex";
import ModalBtn from '../UI/button/ModalBtn.vue';
// import DialogMenuButton from "./DialogMenuButton";

export default {
  name: "DialogMenu",

  components: { 
    // DialogMenuButton,
     ModalBtn 
    },

  computed: {
    ...mapGetters("localization", ["loc"]),
    ...mapState("dialogMenu", ["Body"]),

    isColumnClass: function () {
      return this.Body.Buttons.length > 2 ? true : false;
    },
  },
  methods: {
    onClick(index){
      window.mp.trigger("dialog::buttonClick", index);
    }
  }
};
</script>

<style lang="scss" scoped>
.dialog-menu {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 99;
  width: 23.368rem;
  padding: 2.211rem 3.526rem 2.579rem;
  font-family: "Akrobat";
  background: rgba(0, 0, 0, 0.95);

  span {
    width: 100%;
    display: block;
    margin-bottom: 1.684rem;
    font-weight: 700;
    font-size: 1.263rem;
    line-height: 1.526rem;
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    white-space: pre-wrap;
    position: relative;
  }

  div {
    display: grid;
    grid-template-rows: 1fr 1fr;
    row-gap: 0.526rem;
    width: 100%;
  }
}
.scale-in-enter-active,
.scale-in-leave-active {
  transition: all 0.4s;
}
.scale-in-enter,
.scale-in-leave-to {
  transform: scale(0.2);
  opacity: 0;
}
</style>
