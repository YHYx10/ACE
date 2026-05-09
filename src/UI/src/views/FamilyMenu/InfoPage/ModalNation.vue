<template>
  <transition name="scale-in">
    <div class="window">
      <div class="dialog-menu">
        <span class="dialog-menu__header">{{ loc('familyMenu_42') }}</span>
        <div :class="[{ column: true }, 'dialog-menu__actions']">
          <ModalInput type="text" v-model="nation" />
          <ModalBtn
            @click="saveNation"
            class="primary"
            :button="{
              text: 'familyMenu_35',
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
  name: 'ModalNation',
  computed: {
    ...mapState('familyMenu', ['infoPage']),
    ...mapGetters('localization', ['loc']),
  },
  data: function() {
    return {
      nation: null,
    }
  },
  methods: {
    closeModal: function() {
      this.$emit('closeModalNation')
    },
    saveNation: function() {
      window.mp.trigger('familyMenu:saveNation', this.nation)
      this.closeModal()
    },
  },
  created() {
    this.nation = this.infoPage.nation
  },
  components: { ModalInput, ModalBtn },
}
</script>

<style lang="scss" scoped>
@import '../modal.scss';

</style>
