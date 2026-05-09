<template>
  <div class="settings-chat">
    <!-- <img src="/img/optionsMenu/settings/lockbig.png" alt="" class="settings-chat-lock"> -->
    <div class="billet-item">
      <div class="title">{{ loc("mmain:setts:chat:censore") }}</div>
      <SettingsSwitch
          :status="settings.censore"
          class="settings-special-switch"
          @onSwitch="setSettingLocal($event, 'censore')
        "/>
    </div>
  </div>
</template>

<script>
import {mapState, mapMutations, mapGetters} from 'vuex'
import SettingsSwitch from '../SettingsSwitch'

export default {
  name: 'Chat',
  computed: {
    ...mapState('optionsMenu', ['settings']),
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    ...mapMutations('optionsMenu', ['setSetting']),
    setSettingLocal(status, name) {
      this.setSetting({name, status})
    }
  },
  components: {
    SettingsSwitch
  }
}
</script>
