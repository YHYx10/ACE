<template>
  <div class="settings-account">
    <div class="settings-account-heading">
      <div class="category">section</div>
      <div class="title">{{ loc('mm_setts_acc_cpwd_t') }}</div>
    </div>
    <div class="settings-account-input">
      <input
          type="password"
          :placeholder="loc('mm_setts_acc_cpwd_pl1')"
          v-model="currentPassword"
      >
    </div>
    <div class="settings-account-input">
      <input type="password"
             :placeholder="loc('mm_setts_acc_cpwd_pl2')"
             v-model="newPassword"
      >
    </div>
    <div class="settings-account-input">
      <input
          type="password"
          :placeholder="loc('mm_setts_acc_cpwd_pl3')"
          v-model="confirmPassword"
      >
    </div>
    <div class="settings-account-send item__btn" @click="changePassword">
      {{ loc('mm_setts_acc_cpwd_bt') }}
    </div>
    <div class="settings-account-email">
      <div class="settings-account-email-heading">
        <div class="category">section</div>
        <div class="title">{{ loc('mm_setts_acc_entpromo') }}</div>
      </div>
      <div class="settings-account-input">
        <input type="text" v-model="newEmail" :placeholder="loc('mm_setts_acc_plpromo')">
        <div class="settings-account-email-button item__btn" @click="usePromo">{{ loc('mm_setts_acc_promo_act') }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import {mapGetters} from 'vuex'

export default {
  name: 'Chat',
  data() {
    return {
      currentPassword: '',
      newPassword: '',
      confirmPassword: '',
      newEmail: ''
    }
  },
  computed: {
    ...mapGetters('localization', ['loc'])
  },
  methods: {
    usePromo() {
      window.mp.triggerServer("usepromoopt", this.newEmail)
      this.newEmail = ''
    },
    changePassword() {
      if (this.newPassword !== this.confirmPassword) {
        this.$store.commit('notifyList/notify', {type: 1, position: 0, message: this.loc('auth_16'), time: 3000});
        return;
      }
      window.mp.triggerServer("mmain:pwd:change", this.currentPassword, this.newPassword)
    }
  }
}
</script>

<style lang="scss">
.settings-account {
  position: relative;
  max-width: 25rem;

  &-heading {
    position: absolute;
    top: -5rem;
    left: 0.7rem;
  }

  &-input {
    position: relative;
    display: flex;
    align-items: center;
    padding: 1rem 0rem 1rem 2rem;
    border: 1px solid;
    border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.1) 50%, rgba(0, 0, 0, 0) 100%) 1;
    background: linear-gradient(90deg, rgba(255, 255, 255, 0) 10%, rgba(255, 255, 255, 0.03) 35%, rgba(12, 16, 10, 0) 100%);
    margin-bottom: 0.5rem;

    &:before {
      content: '';
      position: absolute;
      height: 1.44rem;
      top: 50%;
      left: 0.7rem;
      transform: translateY(-50%);
      color: #fff;
      background: #fff;
      border: 1px solid #fff;
      box-shadow: 0px 0px 14px rgba(255, 255, 255, 0.55);
    }

    input {
      width: 100%;
      background: none;
      color: rgba(255, 255, 255, 0.5);
      font-weight: 300;
      font-size: 0.88rem;
      line-height: 1.05rem;
      letter-spacing: 0.04rem;
      padding-right: 6rem;
      text-transform: none !important;
      &::placeholder {
        text-transform: uppercase !important;
      }
    }
  }

  &-send {
    max-width: 14rem;
    margin-top: 1rem;
  }

  &-email {
    margin-top: 2rem;

    &-heading {
      margin: 0 0 1rem 0.7rem;
    }
  }
}
</style>
