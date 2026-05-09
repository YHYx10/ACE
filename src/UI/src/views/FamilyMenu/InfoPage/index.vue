<template>
  <div class="info-page">
    <div class="head">
      <TitleItem
        :name="'The name of the community'"
        :value="[organizationName.value]"
        :fontSize="48"
      />
      <div class="btns-wrap">
        <div class="container-item" @click="showRating">
          <div class="text">{{ loc('familyMenu_16') }}</div>
        </div>
        <div class="container-item" @click="setModalRules">
          <div class="text">{{ loc('familyMenu_17') }}</div>
        </div>
      </div>
    </div>
    <div class="tiles">
      <div class="upper-row">
        <div class="house scale">
          <button @click="editHouse">Hausmanagement</button>
        </div>
        <div class="about">
          <div class="bio container-item">
            <img
              src="/img/familyMenu/icon-settings.svg"
              @click="setModalBio"
              alt=""
              class="icon settings"
            />
            <div class="title">Biographie der Organisationen</div>
            <div class="text">{{ infoPage.biography }}</div>
          </div>
          <div class="address container-item">
            <TitleItem
              :name="'Housing class'"
              :value="[infoPage.houseType]"
              :fontSize="24"
            />
            <TitleItem
              :name="'Address'"
              :value="[infoPage.houseAdress]"
              :fontSize="24"
            />
          </div>
        </div>
        <div class="balance container-item">
          <div class="bank-img" />
          <div class="title">Familienbalance</div>
          <div class="value">
            ${{ infoPage.bankBalance.toLocaleString('en-US') }}
          </div>
          <div class="operation">
            <DefaultBtn @click="setModalMoney(1)" class="default-btn"
              >Wieder auffüllen</DefaultBtn
            >
            <DefaultBtn @click="setModalMoney(0)" class="withdraw"
              >Nehmen aus</DefaultBtn
            >
          </div>
        </div>
      </div>
      <div class="lower-row">
        <div class="container-item">
          <div class="ico leader" />
          <TitleItem
            :name="`Community leaders: ${leaders.length}`"
            :value="leaders.map((v) => v.nickname)"
            :fontSize="32"
          />
        </div>
        <div class="container-item">
          <img
            src="/img/familyMenu/icon-settings.svg"
            @click="setModalNation"
            alt=""
            class="icon settings"
          />
          <div class="ico flag" />
          <TitleItem
            :name="'nationality'"
            :value="[infoPage.nation]"
            :fontSize="48"
          />
        </div>
        <div class="container-item">
          <img
            src="/img/familyMenu/icon-info.svg"
            @click="showRating"
            alt=""
            class="icon"
          />
          <div class="ico rate" />
          <TitleItem
            :name="'rating'"
            :value="[`${getFamilyRank} place`]"
            :fontSize="48"
          />
        </div>
        <div class="container-item">
          <img
            src="/img/familyMenu/icon-info.svg"
            alt=""
            class="icon"
            @click="setCurrentPage('MemberPage')"
          />
          <div class="ico member" />
          <TitleItem
            :name="'Participants'"
            :value="[`${getMembersCount} man`]"
            :fontSize="48"
          />
        </div>
      </div>
    </div>
    <div class="body" v-show="false">
      <div class="body__item" id="leadersPage">
        <div
          v-if="leaders.length > 1"
          class="btn btn-watch"
          @click="setModalLeaders"
        ></div>
        <div class="icon"></div>
        <div class="info">
          <div class="title">
            {{ loc('familyMenu_18') }}: {{ leaders.length }}
          </div>
          <div class="desc">{{ leaders[0] && leaders[0].nickname }}</div>
        </div>
      </div>
      <div class="body__item" id="nacionPage">
        <div
          v-if="isLeader"
          class="btn btn-options"
          @click="setModalNation"
        ></div>
        <div class="icon"></div>
        <div class="info">
          <div class="title">{{ loc('familyMenu_19') }}</div>
          <div class="desc">{{ infoPage.nation }}</div>
        </div>
      </div>
      <div class="body__item" id="paymentPage">
        <div class="icon"></div>
        <div class="info">
          <div class="title">{{ loc('familyMenu_20') }}:</div>
          <div class="desc">$ {{ getIncome }}</div>
        </div>
      </div>
      <div class="body__item" id="membersPage">
        <div class="icon"></div>
        <div class="info">
          <div class="title">{{ loc('familyMenu_21') }}</div>
          <div class="desc">
            {{ getMembersCount }} {{ loc('familyMenu_22') }}
          </div>
        </div>
      </div>
      <div class="body__item" id="bioPage">
        <div v-if="isLeader" class="btn btn-options" @click="setModalBio"></div>
        <div
          class="img"
          style="background-image: url(/img/familyMenu/home-img.png)"
        >
          <div class="btn btn-to-house" @click="editHouse">
            {{ loc('familyMenu_23') }}
          </div>
          <div class="desc">{{ infoPage.houseType }}</div>
          <div class="adress">{{ infoPage.houseAdress }}</div>
        </div>
        <div class="info">
          <div class="desc">{{ loc('familyMenu_24') }}</div>
          <div class="bio-text">{{ infoPage.biography }}</div>
        </div>
      </div>
      <div class="body__item" id="activityPage">
        <div class="desc">{{ loc('familyMenu_25') }}</div>
        <div class="circle-wrap">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 175 175"
            fill="none"
          >
            <circle
              cx="87.5"
              cy="87.5"
              r="82.5"
              stroke="white"
              stroke-opacity="0.1"
              stroke-width="10"
            />
            <circle
              stroke-linecap="round"
              cx="87.5"
              cy="87.5"
              r="82.5"
              stroke="#D5FF03"
              stroke-width="10"
              :stroke-dasharray="calcCircumference"
              :stroke-dashoffset="circleValue"
            />
          </svg>
          <div class="value">{{ activeOnline }}%</div>
        </div>
        <div class="online">
          {{ getOnlineMembersCount }} {{ loc('familyMenu_26') }}
        </div>
      </div>
      <div class="body__item" id="ratingPage">
        <div class="value">{{ getFamilyRank }}</div>
        <div class="info">
          <div class="title">{{ loc('familyMenu_27') }}:</div>
          <div class="desc">{{ loc('familyMenu_28') }}</div>
        </div>
      </div>
      <div class="body__item" id="buissPage">
        <div class="value">{{ getBusinessCount }}</div>
        <div class="title">{{ loc('familyMenu_29') }}</div>
      </div>
      <div class="body__item" id="bankPage">
        <div class="icon"></div>
        <div class="info">
          <div class="title">{{ loc('familyMenu_30') }}</div>
          <div class="desc">$ {{ infoPage.bankBalance }}</div>
        </div>
        <div class="btn btn-put" @click="setModalMoney(1)">
          <div class="icon"></div>
          <div class="text">{{ loc('familyMenu_31') }}</div>
        </div>
        <div class="btn btn-take" @click="setModalMoney(0)">
          <div class="icon"></div>
          <div class="text">{{ loc('familyMenu_32') }}</div>
        </div>
      </div>
    </div>
    <modal-leaders
      v-if="modalLeaders"
      :leaders="leaders"
      @closeModalLeaders="setModalLeaders"
    />
    <modal-nation v-if="modalNation" @closeModalNation="setModalNation" />
    <modal-money
      v-if="modalMoney.show"
      :type="modalMoney.type"
      @closeModalMoney="setModalMoney"
    />
    <modal-rules v-if="modalRules" @closeRulesModal="setModalRules" />
    <modal-bio v-if="modalBio" @closeBioModal="setModalBio" />
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'

import ModalLeaders from './ModalLeaders'
import ModalNation from './ModalNation'
import ModalMoney from './ModalMoney'
import ModalRules from './ModalRules'
import ModalBio from './ModalBio'
import TitleItem from '../components/TitleItem.vue'
import DefaultBtn from '../../UI/button/DefaultBtn.vue'

export default {
  name: 'InfoPage',

  components: {
    ModalLeaders,
    ModalNation,
    ModalMoney,
    ModalRules,
    ModalBio,
    TitleItem,
    DefaultBtn,
  },

  data: function() {
    return {
      modalLeaders: false,
      modalNation: false,
      modalMoney: {
        show: false,
        type: null,
      },
      modalRules: false,
      modalBio: false,
    }
  },

  computed: {
    ...mapState('familyMenu', ['isLeader', 'infoPage']),
    ...mapState('familyMenu/controlPage', ['organizationName']),
    ...mapState('familyMenu/ratingPage', ['show']),
    ...mapState('familyMenu/membersPage', ['members']),
    ...mapState('familyMenu/propertyPage', ['propertyList']),
    ...mapGetters('localization', ['loc']),
    leaders: function() {
      return this.members.filter((element) => element.rank === 0)
    },
    activeOnline: function() {
      let number = (this.getOnlineMembersCount / this.getMembersCount) * 100
      return number.toFixed()
    },
    calcCircumference: function() {
      let number = 2 * Math.PI * 82.5
      return number
    },
    circleValue: function() {
      const maxCircleValue = this.calcCircumference
      const maxCircleValuePer =
        (maxCircleValue / 100 / this.getMembersCount) *
        100 *
        this.getOnlineMembersCount
      const curCircleVal = maxCircleValue - maxCircleValuePer

      return curCircleVal
    },
    getBusinessCount: function() {
      return this.propertyList.length
    },
    getMembersCount: function() {
      return this.members.length
    },
    getOnlineMembersCount: function() {
      return this.members.filter((element) => element.online).length
    },
    getIncome: function() {
      return this.propertyList.reduce((sum, item) => sum + item.income, 0)
    },
    getFamilyRank: function() {
      return this.$store.getters['familyMenu/ratingPage/getFamilyRank'](
        this.infoPage.familyId
      )
    },
  },

  methods: {
    ...mapMutations('familyMenu', ['toggleNav', 'setCurrentPage']),
    ...mapMutations('notifyList', ['notify']),
    setModalLeaders: function() {
      this.modalLeaders = !this.modalLeaders
    },
    setModalNation: function() {
      this.modalNation = !this.modalNation
    },
    setModalMoney: function(type) {
      this.modalMoney.show = !this.modalMoney.show
      this.modalMoney.type = type
    },
    setModalRules: function() {
      this.modalRules = !this.modalRules
    },
    setModalBio: function() {
      this.modalBio = !this.modalBio
    },
    editHouse: function() {
      if(!this.infoPage.houseType) return this.notify({type: 1, message: 'You dont have a family house', time: 4000})
      this.setCurrentPage('HomePage')
      window.mp.trigger('familyMenu:editHouse')
    },
    showRating: function() {
      this.toggleNav(false)
      this.setCurrentPage('RatingPage')
    },
  },
}
</script>

<style lang="scss" scoped>
div,
span,
button {
  font-family: 'Akrobat';
  color: #ffffff;
}
.info-page {
  display: flex;
  flex-flow: column;
  align-items: center;
  width: 100%;
  margin-top: 9.259vh;
  .scale {
    transition: 0.2s;
    &:hover {
      transform: scale(1.04);
    }
  }

  .container-item {
    border: 0.093vh solid rgba(255, 255, 255, 0.09);
    position: relative;
    background: rgba(255, 255, 255, 0.02);
    transition: 0.2s;
    &:hover {
      transform: scale(1.04);
    }
    .icon {
      position: absolute;
      transition: 0.1s;
      width: 2.407vh;
      &:hover {
        filter: invert(50%);
      }
      &.settings:hover {
        transform: rotate(35deg);
      }
    }
  }
  .head {
    width: 143.333vh;
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 1.481vh;
    .title {
      display: flex;
      flex-flow: column;
    }
    .btns-wrap {
      display: flex;
      align-items: center;
      justify-content: flex-end;
      gap: 1.852vh;
      .container-item {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 24.815vh;
        height: 7.407vh;
        font-size: 1.481vh;
        &:hover {
          overflow: hidden;
          &::after {
            content: '';
            position: absolute;
            width: 13.889vh;
            height: 13.889vh;
            left: 4.722vh;
            top: 2.13vh;

            background: #ffffff;
            opacity: 0.25;
            filter: blur(8.241vh);
          }
        }
      }
      .btn {
        background: radial-gradient(
          82.95% 1381.5% at 3.97% 16.22%,
          #333333 0%,
          #0f0f13 100%
        );
        border: 0.093vh solid #97908b;
        padding: 0.8rem 1rem;
        margin-left: 1rem;
        font-family: 'Roboto';
        font-style: normal;
        font-weight: normal;
        font-size: 1rem;
        line-height: 1.15rem;
        text-transform: uppercase;
        &:hover {
          box-shadow: 0 0 1rem rgba(255, 255, 255, 0.2);
        }
        &:first-child {
          margin-left: 0;
        }
        .icon {
          width: 1.2rem;
          height: 1.2rem;
          margin-right: 0.5rem;
          background-size: contain;
          background-repeat: no-repeat;
          background-position: center;
        }
        &.btn-rating {
          .icon {
            background-image: url('/img/familyMenu/btn-rating.svg');
          }
        }
        &.btn-quests {
          .icon {
            background-image: url('/img/familyMenu/btn-quests.svg');
          }
        }
        &.btn-charter {
          .icon {
            background-image: url('/img/familyMenu/btn-charter.svg');
          }
        }
      }
    }
  }

  .tiles {
    display: flex;
    flex-direction: column;
    width: 143.333vh;
    gap: 1.852vh;

    .upper-row {
      display: flex;
      gap: 1.852vh;
      height: 36.204vh;
      .house {
        background-image: url(/img/familyMenu/home-img.png);
        background-repeat: no-repeat;
        background-size: cover;
        width: 59.352vh;
        position: relative;
        button {
          width: 23.241vh;
          height: 5.833vh;
          display: flex;
          justify-content: center;
          align-items: center;
          position: absolute;
          bottom: 1.852vh;
          left: 1.852vh;
          font-family: 'Akrobat';
          font-weight: 700;
          font-size: 1.852vh;
          line-height: 2.222vh;
          text-transform: uppercase;
          color: #ffffff;
          background: rgba(255, 255, 255, 0.05);
          box-shadow: 0vh 0vh 1.389vh rgba(0, 0, 0, 0.4);
          &:hover {
            background: rgba(255, 255, 255, 0.1);
          }
        }
      }

      .about {
        display: flex;
        flex-direction: column;
        gap: 1.852vh;
        .bio {
          width: 55.463vh;
          height: 24.259vh;
          display: flex;
          flex-direction: column;
          justify-content: flex-start;
          padding: 3.148vh 3.704vh 3.148vh 2.778vh;
          font-family: 'Akrobat';
          font-style: normal;
          font-weight: 700;
          color: #ffffff;
          gap: 1.852vh;
          img {
            top: 3.241vh;
            right: 3.704vh;
          }
          .title {
            font-size: 2.222vh;
            line-height: 2.685vh;
            text-transform: uppercase;
          }
          .text {
            font-size: 1.481vh;
            line-height: 1.759vh;
            text-transform: none;
          }
        }

        .address {
          display: flex;
          align-items: center;
          gap: 6.481vh;
          padding-left: 2.778vh;
          padding-top: 0.926vh;
          height: 10.093vh;
        }
      }

      .balance {
        width: 24.815vh;
        display: flex;
        flex-direction: column;
        align-items: center;
        .bank-img {
          margin-top: 0.556vh;
          margin-bottom: 0.463vh;
          width: 21.944vh;
          height: 20.093vh;
          position: relative;
          display: flex;
          align-items: center;
          justify-content: center;
          background: url(/img/familyMenu/bank-bg.svg);
          background-repeat: no-repeat;
          background-size: cover;
          &::after {
            content: '';
            position: relative;
            width: 12.778vh;
            height: 11.759vh;
            background: url('/img/familyMenu/bank.svg');
            background-repeat: no-repeat;
            background-size: cover;
          }
        }
        .title {
          font-weight: 700;
          font-size: 1.111vh;
          line-height: 1.296vh;
          text-align: center;
          text-transform: uppercase;
          color: rgba(255, 255, 255, 0.55);
        }
        .value {
          font-weight: 700;
          font-size: 4.444vh;
          line-height: 5.37vh;
          text-align: center;
          text-transform: uppercase;
          color: #ffffff;
        }
        .operation {
          display: flex;
          gap: 0.463vh;
          margin-top: 2.13vh;
          align-items: center;

          button {
            width: 9.815vh;
            height: 2.778vh;
            font-size: 1.296vh;
            line-height: 1.574vh;
            font-weight: 600;
            text-transform: uppercase;
            &.withdraw {
              background: linear-gradient(
                180deg,
                rgba(71, 44, 132, 0.25) 0%,
                rgba(75, 0, 130, 0.25) 100%
              );
              &:hover {
                background: linear-gradient(
                  180deg,
                  rgba(255, 8, 21, 0.35) 0%,
                  rgba(75, 0, 130, 0.25) 100%
                );
              }
            }
          }
        }
      }
    }

    .lower-row {
      display: flex;
      gap: 1.852vh;

      .container-item {
        width: 34.444vh;
        height: 18.889vh;
        display: flex;
        align-items: center;
        gap: 0.833vh;
        overflow: hidden;
        .icon {
          top: 1.852vh;
          right: 1.852vh;
        }

        .ico {
          margin-left: 1.019vh;
          width: 7.407vh;
          height: 7.407vh;
          background-size: contain;
          background-repeat: no-repeat;
          background-position: center;
          position: relative;
          &::before {
            content: '';
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 16.667vh;
            height: 16.667vh;
            opacity: 0.04;
            background-size: cover;
            background-repeat: no-repeat;
            background-position: center;
          }
          &.leader {
            background-image: url(/img/familyMenu/ico-leader.svg);
            &::before {
              background-image: url(/img/familyMenu/ico-leader.svg);
            }
          }
          &.flag {
            background-image: url(/img/familyMenu/ico-flag.svg);
            &::before {
              background-image: url(/img/familyMenu/ico-flag.svg);
            }
          }
          &.rate {
            background-image: url(/img/familyMenu/ico-rate.svg);
            &::before {
              background-image: url(/img/familyMenu/ico-rate.svg);
            }
          }
          &.member {
            background-image: url(/img/familyMenu/ico-member.svg);
            &::before {
              background-image: url(/img/familyMenu/ico-member.svg);
            }
          }
        }
      }
    }
  }
}
</style>
