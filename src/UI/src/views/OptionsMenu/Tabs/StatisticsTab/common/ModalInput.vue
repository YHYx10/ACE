<template>
  <transition name="scale-in">
    <div class="window">
      <div class="dialog-menu">
        <span class="dialog-menu__header">{{ 'Enter the promotional code' }}</span>
        <div :class="[{ column: true }, 'dialog-menu__actions']">
          <ModalInput type="text" v-model="promoCode" />
          <ModalBtn
            @click="send"
            class="primary"
            :button="{
              text: 'Activate',
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
import { mapGetters } from 'vuex'
import ModalBtn from '../../../../UI/button/ModalBtn.vue'
import ModalInput from '../../../../FamilyMenu/components/ModalInput.vue'
export default {
  name: 'ModalPromo',
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  data: function() {
    return {
      promoCode: '',
    }
  },
  methods: {
    closeModal: function() {
      this.$emit('close')
    },
    send: function() {
      window.mp.triggerServer("promocode:setup", this.promoCode);
      this.closeModal()
    },
  },
  components: { ModalInput, ModalBtn },
}
</script>

<style lang="scss" scoped>
@import '../../../../FamilyMenu/modal.scss';

</style>
