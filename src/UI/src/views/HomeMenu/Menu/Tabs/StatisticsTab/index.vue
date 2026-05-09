<template>
  <div class="statistics-tab">
    <div class="statistics-tab__left">
      <div class="general">
        <div class="username general-props">
          <div class="general-prop">
            <img src="img/optionsMenu/statisticsTab/icons/username.svg" class="statistics-tab__left-icon">
            <div class="general-prop_text">
              <div class="statistics-tab__left-title">Name Surname</div>
              <div class="statistics-tab__left-value">{{ statistics.username }}</div>
            </div>
          </div>
        </div>
        <div class="progress general-props">
          <div class="general-prop">
            <img src="img/optionsMenu/statisticsTab/icons/lvl.svg" class="statistics-tab__left-icon">
            <div class="general-prop_text">
              <div class="statistics-tab__left-title">level</div>
              <div class="statistics-tab__left-value">{{ statistics.level }}</div>
            </div>
          </div>
          <div class="general-prop">
            <img src="img/optionsMenu/statisticsTab/icons/exp.svg" class="statistics-tab__left-icon">
            <div class="general-prop_text">
              <div class="statistics-tab__left-title">exp</div>
              <div class="statistics-tab__left-value">{{ statistics.exp }}/<span>{{ maxExp }}</span></div>
            </div>
          </div>
        </div>
      </div>
      <div class="info">
        <info-item
            v-for="(item, index) in infoItems"
            :key="index"
            :item="item"
        />
      </div>
    </div>
    <div class="statistics-tab__center">
      <div class="action">
        <action-item
            v-for="(item, index) in actionItems"
            :key="index"
            :item="item"
        />
      </div>
      <button class="item family">
        <div class="item__info">
          <div class="category">category</div>
          <div class="title">My family</div>
          <div class="subtitle"> You have no family</div>
        </div>
      </button>
    </div>
    <div class="statistics-tab__right">
      <div class="item report">
        <div class="item__info">
          <div class="category">category</div>
          <div class="title">report</div>
          <div class="subtitle">Here you can contact the server administration with requests and questions!</div>
        </div>
        <button class="report-btn">Contact the administration</button>
      </div>
      <div class="extra">
        <extra-item
            v-for="(item, index) in extraItems"
            :key="index"
            :item="item"
        />
      </div>
    </div>
    <transition-group tag="div" name="modal">
      <transport-modal :key="1" v-if="isTransportModal" @close="isTransportModal = false"/>
      <business-modal :key="2" v-if="isBusinessModal" @close="isBusinessModal = false"/>
    </transition-group>
  </div>
</template>

<script>
import {mapState, mapGetters} from 'vuex'
import InfoItem from './common/InfoItem'
import ExtraItem from './common/ExtraItem'
import ActionItem from './common/ActionItem'
import TransportModal from './TransportModal'
import BusinessModal from './BusinessModal'

export default {
  name: 'StatisticsTab',

  components: {
    InfoItem,
    ExtraItem,
    ActionItem,
    TransportModal,
    BusinessModal,
  },

  data: function () {
    return {
      isTransportModal: false,
      isBusinessModal: false,
      infoItems: [
        {title: 'mmain:stats:info:number', icon: 'telephone', key: "phone"},
        {title: 'mmain:stats:info:lic', icon: 'license', key: 'licenses'},
        {title: 'mmain:stats:info:work', icon: 'suitcase', key: 'work'},
        {title: 'mmain:stats:info:passp', icon: 'passport', key: 'passportNumber'},
        {title: 'mmain:stats:info:bacc', icon: 'museum', key: 'bankCount'},
        {title: 'mmain:stats:info:rang', icon: 'rank', key: 'rank'},
        {title: 'mmain:stats:info:org', icon: 'batallia', key: 'organization'},
        {
          title1: 'mmain:stats:info:fams1',
          title2: 'mmain:stats:info:fams2',
          title3: 'mmain:stats:info:fams3',
          icon: 'hands',
          key: 'maritalStatus'
        }
      ],
      extraItems: [
        {title: 'alert', icon: 'alert', key: 'alert', keyFirst: 'warns', keySecond: 'bans', textFirst: 'mmain:stats:warn', textSecond: 'mmain:stats:ban'},
        {title: 'premium', icon: 'premium', icon1: 'premium1', key: 'premium', text: ''},
      ],
      actionItems: [
        {
          title: 'mmain:stats:house', bg: 'home', key: 'house', event: () => {
            window.mp.triggerServer("house:OwnerInteracted");
            window.mp.trigger("cef:mmenu:close");
          }
        },
        {
          title: 'mmain:stats:biz', bg: 'bank', key: 'business', event: () => {
            this.isBusinessModal = true;
            window.mp.triggerServer("mmenu:products:get", this.property.business.number);
          }
        },
        {
          title: 'mmain:stats:vehs', bg: 'transport', key: 'transport', event: () => {
            this.isTransportModal = true
          }
        },
      ]
    }
  },

  computed: {
    ...mapState('optionsMenu', ['statistics', 'property']),
    ...mapGetters('localization', ['loc']),
    maxExp() {
      return this.statistics.level * 3 + 3;
    }
  },

  methods: {
    formatTime: function (time) {
      const date = new Date(time)
      return date.toLocaleDateString('ru-RU')
    },
  }
}
</script>

<style lang="scss">
.statistics-tab {
  display: flex;
  margin-top: 2.5rem;

  .item {
    position: relative;
    min-width: 19.9rem;
    max-width: 22rem;
    flex: 1 1 33%;
    display: flex;
    flex-flow: column nowrap;
    justify-content: space-between;
    padding: 1.2rem 2rem 1rem 2rem;
    border: 1px solid;
    border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.1) 69%, rgba(0, 0, 0, 0) 91%) 1;
    transition: transform 0.4s ease;

    &:hover {
      transform: scale(1.04);
    }

    &:not(:last-child) {
      margin: 0 0 0.75rem 0;
    }

    &__info {
      text-align: left;
    }

    .category:before {
      left: -1rem;
    }

    &.family {
      margin-left: 1.38rem;
      background: url('/img/optionsMenu/statisticsTab/backs/family.png') center / cover no-repeat;

      & .subtitle {
        border: 2px solid rgba(255, 255, 255, 0.55);
        padding: 0.5rem 1.5rem;
        border-radius: 2rem;
        display: inline-block;
      }

      &:after {
        content: '';
        background: url("/img/optionsMenu/statisticsTab/backs/family_after.png") center / contain no-repeat;
        position: absolute;
        bottom: -3.5rem;
        left: -1rem;
        width: 14rem;
        height: 10rem;
      }
    }

    &.report {
      background: url('/img/optionsMenu/statisticsTab/backs/report.png') center / cover no-repeat;

      &:hover {
        transform: scale(1);
      }

      & .report-btn {
        letter-spacing: 0.03em;
        color: #fff;
        padding: 0.5rem 2rem;
        margin: 0 -1rem;
        background: linear-gradient(180deg, #301934  0%, #591b87 100%);
        box-shadow: 0px 0px 15px rgba(71, 44, 132, 0.5), inset 0px 0px 17.9943px rgba(75, 0, 130, 0.86);
        transition: 0.3s ease;

        &:hover {
          transform: scale(1.01);
          box-shadow: 0px 0px 50px rgba(71, 44, 132, 0.5), inset 0px 0px 17.9943px rgba(75, 0, 130, 0.86);
        }
      }
    }
  }

  &__left {
    display: flex;
    flex-direction: column;
    position: relative;

    &-title {
      font-size: 0.65rem;
      line-height: 0.8rem;
      color: rgba(255, 255, 255, 0.55);
      letter-spacing: 0.05em;
      margin: 0 0 0.25rem 0;
    }

    &-value {
      font-size: 0.85rem;
      line-height: 1rem;
      font-weight: 600;
      color: #fff;
      letter-spacing: 0.03em;

      & > span {
        color: rgba(255, 255, 255, 0.55);
      }
    }

    &-icon {
      position: relative;
      width: 1.5rem;
      height: 1.5rem;
      margin-right: 0.83rem;
    }

    .general {
      &-props {
        display: flex;
        justify-content: space-between;
        width: 100%;
        background: rgba(255, 255, 255, 0.03);
        padding: 1.1rem 3.8rem 1.1rem 1.6rem;

        &.progress {
          margin-top: 1rem;

          & .general-prop:first-child {
            margin-right: 2rem;
          }
        }
      }

      &-prop {
        display: flex;
        align-items: center;
      }
    }

    .info {
      display: block;
      margin: 0 0 1.9rem 0;
      height: 26.2rem;
      overflow: scroll;
      -ms-overflow-style: none;
      scrollbar-width: none;

      &::-webkit-scrollbar {
        display: none;
      }
    }
  }

  &__center {
    display: flex;
    margin: 0 1.38rem 0 2rem;
    height: 39.8rem;

    & .action {
      display: flex;
      flex-flow: column nowrap;
    }
  }

  &__right {
    display: flex;
    flex-flow: column nowrap;
    height: 39.8rem;
  }

  &__layer {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(17, 20, 27, 0.8);
  }
}
</style>
