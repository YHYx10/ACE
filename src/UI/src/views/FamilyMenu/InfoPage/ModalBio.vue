<template>
  <div class="modal-bio modal-wrap">
    <div class="content">
      <Back @click="closeModal" />
      <div class="title">{{ loc('familyMenu_33') }}</div>
      <div class="description">{{ loc('familyMenu_34') }}</div>
      <div class="text-info">
        <textarea
          class=""
          :placeholder="loc('familyMenu_37')"
          v-model="currentTextBio"
        />
      </div>

      <div class="row">
        <DefaultBtn @click="setBio">SPEICHERN</DefaultBtn>
        <DefaultBtn @click="cancelEdit" class="secondary">STORNIEREN</DefaultBtn>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import DefaultBtn from '../../UI/button/DefaultBtn.vue'
import Back from '../components/Back.vue'
export default {
  name: 'ModalBio',
  data: function() {
    return {
      currentTextBio: null,
    }
  },
  computed: {
    ...mapState('familyMenu', ['isLeader', 'infoPage']),
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    closeModal: function() {
      this.$emit('closeBioModal')
    },
    cancelEdit: function() {
      this.currentTextBio = this.infoPage.biography
    },
    setBio: function() {
      this.closeModal()
      window.mp.trigger('familyMenu:setBio', this.currentTextBio)
    },
  },
  mounted() {
    this.cancelEdit()
  },
  components: { Back, DefaultBtn },
}
</script>

<style lang="scss" scoped>

div,
span,
button,
textarea {
  font-family: 'Akrobat';
  font-style: normal;
  text-transform: uppercase;
  color: #ffffff;
}
.modal-bio {
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.97);
  display: flex;
  align-items: center;
  justify-content: center;
  .content {
    width: 94.259vh;
    display: flex;
    flex-direction: column;
    .title {
      margin-top: 1.481vh;
      font-weight: 700;
      font-size: 4.444vh;
      line-height: 5.37vh;
    }
    .description {
      font-weight: 700;
      font-size: 1.852vh;
      line-height: 2.222vh;
    }

    .wrap {
      background: rgba(255, 255, 255, 0.03);
      border-radius: 0.278vh;
    }
    .text-info {
      margin-top: 3.056vh;
      height: 46.667vh;
      position: relative;
      overflow: hidden;
      textarea {
        width: 100%;
        height: 100%;
        font-weight: 700;
        font-size: 1.852vh;
        line-height: 2.222vh;
        resize: none;
        border: none;
        padding: 1.952vh 2.322vh;
        outline: none;
        text-transform: none;
        border-radius: 0.278vh;
        background: rgba(255, 255, 255, 0.03);
        &::placeholder {
          color: rgba(255, 255, 255, 0.4);
          text-transform: uppercase;
        }
      }

      &::before {
        content: '';
        position: absolute;
        width: 77.963vh;
        height: 77.963vh;
        left: 50%;
        top: 50%;
        transform: translate(-50%, -50%);
        background: rgba(255, 255, 255, 0.18);
        opacity: 0.25;
        filter: blur(9.259vh);
        z-index: -1;
      }
    }
    .row {
      margin-top: 1.852vh;
      display: flex;
      gap: 1.852vh;

      button {
        width: 27.315vh;
        height: 6.944vh;
        font-weight: 700;
        font-size: 2.222vh;
        line-height: 2.685vh;
        &.secondary {
          background: rgba(255, 255, 255, 0.05);
          &:hover {
            background: rgba(255, 255, 255, 0.1);
          }
        }
      }
    }
  }
}
</style>
