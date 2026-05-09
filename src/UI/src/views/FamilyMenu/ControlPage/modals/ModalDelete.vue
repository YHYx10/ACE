<template>
  <transition name="scale-in">
    <div class="window">
      <div class="dialog-menu">
        <span class="dialog-menu__header">{{ loc('familyMenu_94') }}</span>
        <div :class="[{ column: true }, 'dialog-menu__actions']">
          <ModalBtn
            @click="deleteOrganization"
            class="primary"
            :button="{
              text: 'familyMenu_74',
            }"
          />
          <ModalBtn
            @click="closeModal"
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
  name: 'ModalDelete',
  methods: {
    closeModal: function() {
      this.$emit('closeModal')
    },
    deleteOrganization: function() {
      window.mp.trigger('familyMenu:deleteOrganization')
      this.$emit('closeModal')
    },
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  components: { ModalBtn },
}
</script>

<style lang="scss" scoped>
@import '../../modal.scss';
</style>
