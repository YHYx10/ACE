<template>
  <div class="settings-interface">
    <div class="billet-item">
      <div class="title">{{ loc("mmain:setts:iface:help") }}</div>
      <SettingsSwitch :status="settings.hint" 
                      @onSwitch="setSettingLocal($event, 'hint')"/>
    </div>
    <div class="billet-item">
      <div class="title">{{ loc("mmain:setts:iface:pname") }}</div>
      <SettingsSwitch :status="settings.showNames" 
                      @onSwitch="setSettingLocal($event, 'showNames')"/>
    </div>
    <div class="billet-item">
      <div class="title">{{ loc("mmain:setts:iface:shud") }}</div>
      <SettingsSwitch :status="settings.showHud" 
                      @onSwitch="setSettingLocal($event, 'showHud')"/>
    </div>
    <div class="billet-item">
      <div class="title">{{ loc("mmain:setts:iface:mmap") }}</div>
      <SettingsSwitch :status="settings.showMiniMap" 
                      @onSwitch="setSettingLocal($event, 'showMiniMap')"/>
    </div>
    <div class="billet-item">
      <div class="title">{{ loc("mmain:setts:iface:drift") }}</div>
      <SettingsSwitch :status="settings.showDrift" 
                      @onSwitch="setSettingLocal($event, 'showDrift')"/>
    </div>
    <div class="billet-item">
      <div class="title">
        {{ loc("mmain:setts:iface:mute") }}
        <div class="settings-interface-input-body">
          <button @click="settings.muteLowLevelValue === 0 ? null : setSetting({name: 'muteLowLevelValue', status: settings.muteLowLevelValue - 1})">-</button>
          <input type="number" class="settings-interface-input" v-model="level" v-bind:style="{width: settings.muteLowLevelValue.toString().length + 'ch'}">
          <button @click="setSetting({name: 'muteLowLevelValue', status: settings.muteLowLevelValue + 1})">+</button>
        </div>
        lvl
      </div>
      <SettingsSwitch :status="settings.muteLowLevel" 
                      @onSwitch="setSettingLocal($event, 'muteLowLevel')"/>
    </div>
    <div class="billet-item">
      <div class="title">{{ loc("mmain:setts:iface:shfrac") }}</div>
      <SettingsSwitch
          :status="settings.showFamilyMembers"
          
          @onSwitch="setSettingLocal($event, 'showFamilyMembers')
      "/>
    </div>
    <div class="billet-item">
      <div class="title">{{ loc("mmain:setts:iface:traffic") }}</div>
      <SettingsSwitch
          :status="settings.trafficOff"
          
          @onSwitch="setSettingLocal($event, 'trafficOff')
    "/>
    </div>
    <div class="billet-item">
      <div class="title">
        {{ loc("mmain:setts:iface:chreload") }}
      </div>
      <div class="settings-interface-reload item__btn" @click="reloadMicro">
        {{ loc("mmain:setts:iface:chreload:tit") }}
      </div>
    </div>
  </div>
</template>

<script>
import SettingsSwitch from '../SettingsSwitch'
import {mapState, mapMutations, mapGetters} from 'vuex'

export default {
  name: 'Interface',
  computed: {
    ...mapState('optionsMenu', ['settings']),
    ...mapGetters('localization', ['loc']),
    level: {
      get() {
        return this.settings.muteLowLevelValue;
      },
      set(val) {
        this.setSetting({name: 'muteLowLevelValue', status: val})
      }
    }
  },
  data() {
    return {
      settingsList: []
    }
  },
  methods: {
    ...mapMutations('optionsMenu', ['setSetting']),
    setSettingLocal(status, name) {
      this.setSetting({name, status})
    },
    reloadMicro() {
      window.mp.trigger('v_reload');
    }
  },
  components: {
    SettingsSwitch
  }
}
</script>

<style lang="scss">
.settings-interface {
  &-input-body {
    display: inline-flex;
    background-color: rgba(#fff, .05);
    border-radius: 1.72rem;
    text-align: center;
    margin: 0 0.3rem;
    padding: 0.5rem 0;

    input {
      width: 1ch;
      max-width: 4ch;
      font-size: 1.1rem;
      font-weight: 700;
      background: none;
      color: #fff;
      &::-webkit-inner-spin-button, &::-webkit-outer-spin-button {
        -webkit-appearance: none;
        margin: 0;
      }
    }

    button {
      font-size: 1.1rem;
      font-weight: 700;
      background: none;
      padding: 0 0.6rem;
      &:first-child {
        color: #DC2028;
      }
      &:last-child {
        color: #5CFF80;
      }
    }
  }
}
</style>
