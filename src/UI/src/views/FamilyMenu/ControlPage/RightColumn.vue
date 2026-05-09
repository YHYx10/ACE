<template>
  <div class="col-right">
    <Title text="Settings chat" style="transform: translate(0, -50%);" />
    <div class="content">
      <div class="about">
     Bearbeiten Sie die Art der Organisationsnachrichten.<br />
Sie werden sich im Chat unterscheiden
      </div>
      <div class="tabs">
        <div
          v-for="item in navList"
          :key="item.id"
          :class="[{ active: item.key === currentTab }, 'button-item']"
          @click="setCurrentTab(item.key)"
        >
          {{ loc(item.text) }}
        </div>
      </div>

      <component
        :is="currentTab"
        class="select"
        :currentIcon="currentIcon"
        :currentColor="currentColor"
        :colorList="colorList"
        @setCurrentColor="setCurrentColor"
        @setCurrentIcon="setCurrentIcon"
      />

      <div class="col">
        <div class="prompt">{{ loc('familyMenu_82') }}</div>
        <ChatPreview :currentColor="currentColor" :currentIcon="currentIcon" />
      </div>

      <div class="btns">
        <DefaultBtn @click="saveChatOptions">Änderungen anwenden</DefaultBtn>
        <DefaultBtn @click="loadOptions">Stornieren</DefaultBtn>
      </div>
    </div>
  </div>
</template>

<script>
import Title from './components/Title.vue'
import { mapGetters, mapState } from 'vuex'
import IconTab from './ChatOptionsTab/IconTab'
import ColorTab from './ChatOptionsTab/ColorTab'
import DefaultBtn from '../../UI/button/DefaultBtn.vue'
import ChatPreview from './ChatOptionsTab/ChatPreview.vue'
export default {
  components: { Title, IconTab, ColorTab, DefaultBtn, ChatPreview },
  computed: {
    ...mapState('familyMenu/controlPage', ['chatOptions']),
    ...mapGetters('localization', ['loc']),
  },
  data: function() {
    return {
      currentTab: null,
      currentColor: null,
      currentIcon: null,
      navList: [
        {
          text: 'familyMenu_85',
          key: 'IconTab',
        },
        {
          text: 'familyMenu_86',
          key: 'ColorTab',
        },
      ],
      colorList: [
        '#FF41E0',
        '#BD31FF',
        '#FF9AEF',
        '#C40000',
        '#D9D000',
        '#A8DC7F',
        '#5FD900',
        '#3FB5E8',
        '#2127BA',
        '#6C72FF',
        '#1A4E65',
        '#9D278A',
        '#7D1EAA',
        '#D680FF',
        '#8D0101',
        '#DEB448',
        '#D0CC61',
        '#3A8302',
        '#07719F',
        '#3139FF',
        '#B4E8FF',
        '#065B80',
        '#C55151',
        '#BE5E28',
        '#FF9052',
        '#FF5D02',
        '#AD800A',
        '#DEA000',
      ],
    }
  },

  methods: {
    setCurrentTab: function(value) {
      this.currentTab = value
    },
    loadOptions: function() {
      this.currentColor = this.chatOptions.currentColor
      this.currentIcon = this.chatOptions.currentIcon
    },
    saveChatOptions: function() {
      window.mp.trigger(
        'familyMenu:saveChatOptions',
        this.currentIcon,
        this.currentColor
      )
    },
    setCurrentColor: function(value) {
      this.currentColor = value
    },
    setCurrentIcon: function(value) {
      this.currentIcon = value
    },
  },

  mounted() {
    this.setCurrentTab('IconTab'), this.loadOptions()
  },
}
</script>

<style lang="scss" scoped>
div,
span,
button,
input {
  font-family: 'Akrobat';
  font-style: normal;
  color: #ffffff;
}
.col-right {
  width: 54.444vh;
  height: 100%;
  .content {
    margin-left: 3.519vh;
  }
  .about {
    margin-top: 1.296vh;
    width: 40.463vh;
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 2.222vh;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.44);
  }

  .tabs {
    display: flex;
    gap: 0.926vh;
    margin-top: 1.667vh;
    .button-item {
      width: 19.259vh;
      height: 6.111vh;
      border: 0.093vh solid rgba(255, 255, 255, 0.09);
      position: relative;
      overflow: hidden;
      display: flex;
      align-items: center;
      justify-content: center;
      text-transform: uppercase;
      font-weight: 700;
      font-size: 1.852vh;
      line-height: 2.222vh;
      &.active::before {
        content: '';
        z-index: -1;
        position: absolute;
        width: 13.889vh;
        height: 13.889vh;
        background: #ffffff;
        top: 0;
        left: 50%;
        transform: translate(-50%, 0);
        opacity: 0.25;
        filter: blur(8.241vh);
      }
    }
  }

  .select {
    margin-top: 1.852vh;
  }

  .col {
    display: flex;
    flex-flow: column;
    margin-top: 2.87vh;

    .prompt {
      width: 100%;
      position: relative;
      margin-bottom: 1.019vh;
      font-weight: 700;
      font-size: 2.222vh;
      line-height: 2.685vh;
    }
  }

  .btns {
    margin-top: 2.778vh;
    display: flex;
    gap: 0.926vh;
    button {
      font-weight: 700;
      font-size: 2.222vh;
      line-height: 2.685vh;
      text-transform: uppercase;
      color: #fff;
    }
    button:nth-child(1) {
      width: 27.315vh;
      height: 6.944vh;
    }
    button:nth-child(2) {
      width: 22.5vh;
      height: 6.944vh;
      background: rgba(255, 255, 255, 0.05);
      &:hover {
        background: rgba(255, 255, 255, 0.12);
      }
    }
  }
}
</style>
