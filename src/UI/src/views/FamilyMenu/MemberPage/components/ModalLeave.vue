<template>
    <transition name="scale-in">
  <div class="window">
      <div class="dialog-menu">
        <span class="dialog-menu__header">{{ loc('familyMenu_73')}}</span>
        <div :class="[{ column: true }, 'dialog-menu__actions']">
          <ModalBtn
            @click="kickMember"
            class="primary"
            :button="{
              text: 'familyMenu_74',
            }"
          />
          <ModalBtn
            @click="setModalLeave"
            :button="{
              text: 'familyMenu_75',
            }"
          />
        </div>
      </div>
  </div>
    </transition>
</template>

<script>
import { mapGetters } from 'vuex'
import ModalBtn from '../../../UI/button/ModalBtn.vue'
export default {
    name: "ModalLeave",
    props: {
        currentMember: Object
    },
    methods: {
        setModalLeave: function () {
            this.$emit("setModalLeave");
        },
        kickMember: function () {
            window.mp.trigger("familyMenu:kickMember", this.currentMember.id);
            this.setModalLeave();
        }
    },
    computed: {
        ...mapGetters("localization", ["loc"])
    },
    components: { ModalBtn }
}
</script>

<style lang="scss" scoped>
@import '../../modal.scss';
</style>
