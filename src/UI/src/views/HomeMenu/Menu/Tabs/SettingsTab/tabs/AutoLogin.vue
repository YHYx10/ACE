<template>
  <div class="settings-account-special">
    <div class="settings-account-special-heading">
      <div class="category">section</div>
      <div class="title">Special opportunities</div>
    </div>
    <div class="settings-account-special-content">
      <div class="settings-account-special-item">
        <div class="title">Enter automatically</div>
        <SettingsSwitch
            :status="setRememberLogin"
            class="settings-account-special-switch"
            @onSwitch="setRememberLogin"
        />
      </div>
      <div class="settings-account-special-item">
        <div class="title">{{ loc("settings:2fa:title") }}</div>
        <SettingsSwitch
            :status="status"
            class="settings-account-special-switch"
            @onSwitch="set2fa"
        />
      </div>
      <template v-if="status">
        <div class="settings-account-special-confirmed" v-if="confirmed">
          <div class="settings-account-special-confirmed_title">{{ loc("settings:2fa:confirmed") }}</div>
          <div class="settings-account-special-confirmed_phone">{{ hiddenNumber }}</div>
        </div>
        <Input2f v-else-if="isShowConfirm" placeholder="settings:2fa:phone:code" @onsend="sendCode"/>
        <Input2f v-else :value="phone" placeholder="settings:2fa:phone:number" @onsend="sendPhone"/>
      </template>
      <button class="settings-account-special-exit">
        <img src="/img/optionsMenu/account/exit.svg" alt="exit" class="settings-account-special-exit_img">
        <span class="settings-account-special-exit_text">{{ loc("settings:account:exit") }}</span>
      </button>
    </div>
  </div>
</template>

<script>
import {mapGetters, mapMutations, mapState} from 'vuex'
import SettingsSwitch from '../SettingsSwitch.vue'
import Input2f from './Input2f.vue'

export default {
  name: 'Chat',
  computed: {
    ...mapGetters("localization", ["loc"]),
    ...mapState("auth", ["isRememberPass", "auth"]),
    ...mapState("optionsMenu/auth2f", ["status", "isShowConfirm", "confirmed", "phone"]),
    hiddenNumber() {
      if (this.phone.length > 4)
        return `+ ${this.phone[0]}${"*".repeat(this.phone.length - 3)}${this.phone[this.phone.length - 2]}${this.phone[this.phone.length - 1]}`;
      else
        return this.phone;
    }
  },
  components: {
    SettingsSwitch,
    Input2f
  },
  methods: {
    ...mapMutations("optionsMenu/auth2f", ["setStatus"]),
    setRememberLogin(status) {
      if (status == this.isRememberPass) return;
      window.mp.trigger("auth:save:pass", this.auth.login, this.auth.password, status);
    },
    set2fa(status) {
      if (status == this.status) return;
      this.setStatus(status);
    },
    sendPhone(value) {
      console.log(value)
    },
    sendCode(value) {
      console.log(value)
    }
  }
}
</script>

<style lang="scss">
.settings-account-special {
  position: relative;

  &-heading {
    position: absolute;
    top: -5rem;
  }

  &-content {
    margin-top: 4rem;
  }

  &-item {
    display: flex;
    flex-wrap: nowrap;
    justify-content: space-between;
    margin: 2rem 0;

    .title {
      font-size: 1.2rem !important;
      line-height: 1.4rem !important;
      color: rgba(255,255,255,0.5) !important;
      margin-right: 1rem;
    }
  }

  &-exit {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 3.5rem;
    background: linear-gradient(270deg, rgba(41, 47, 51, 0.9) 0%, rgba(20, 21, 23, 0.9) 100%);
    border: 1px solid rgba(255, 255, 255, 0.05);

    &:hover {
      background: linear-gradient(270deg, rgba(78, 88, 95, 0.9) 0%, rgba(20, 21, 23, 0.9) 100%);
    }

    &_img {
      width: 1.4rem;
      height: 1.4rem;
      margin-right: 0.9rem;
    }

    &_text {
      color: #fff;
      font-size: 1rem;
      letter-spacing: 0.03em;
    }
  }

  &-confirmed {
    margin-top: 1.5rem;

    &_title {
      color: #B6D300;
      letter-spacing: 0.05rem;
    }

    &_phone {
      font-size: 1.2rem;
      letter-spacing: 0.08rem;
      margin-top: .3rem;
    }
  }
}
</style>
