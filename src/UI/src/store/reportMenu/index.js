export default {
  namespaced: true,
  state: {
    mySettings: {
      name: 'Milt Devovich'
    },
    answersTemplates: [
      {
        text: 'I was glad to help!'
      }, // при нажатии на ответ он должен в инпут поле "Ваше сообщение" вставляться
      {
        text: 'A good question, please give me a minute, I will clarify the information from (developers, engineers, colleagues) '
      },
      {
        text: 'This error is recorded, I apologize.We have where to grow, thanks for your appeal!'
      },
      {
        text: 'The situation is unpleasant and I understand what you are, but fortunately I will not allow this to happen'
      },
      {
        text: 'It was nice to help, if there are questions, be sure to ask, I will help!'
      },
      {
        text:'Please tell me, which version of the platform and configurations do you use?'
      },
      {
        text: 'Does any mistake appear?Describe step -by -step actions leading to the problem'
      },
      {
        text: 'Tell me, please, which version of the platform do you use?'
      },
      {
        text: 'It was nice to help.'
      },
      {
        text: 'All the best!'
      }
    ],

    messageModel: '',
    staffChatMessages: [],
    penalties: [],
    penaltiesLoading: false,
    penaltiesSearched: false,
    organizations: [],
    organizationsLoading: false,
    organizationsError: '',
    businesses: [],
    businessesLoading: false,
    administrators: [],
    administratorsLoading: false,
    administratorsError: '',
    teleports: [],
    teleportsLoading: false,
    families: [],
    familiesLoading: false,
    familiesError: '',
    commands: [],
    commandsLoading: false,
    commandsError: '',
    eventEntries: [],
    eventEntriesLoading: false,
    murders: [],
    murdersLoading: false,
    arenaSessions: [],
    arenaLoading: false,

    reportsInQueue: 23,
    reportsAnswered: 235,
    reports: [
      {
        id: 5467,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        // 0 - обычный
        // 1 - vip
        // 2 - media
        // 3 - хз в душе не ебу, пусть будет лидер?
        reporterStatus: 1,
        unreadMessages: 0,
        messages: [
          // если sender === reporterName, тогда сообщение слева, иначе справа
          // если sender === mySettings.Name, тогда пишет "Ваш ответ:", иначе "Ответ {{ sender }}:"
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Vasya Devovich',
            text: 'Use the "N" key on your keyboard'
          }
        ]
      },
      {
        id: 5468,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 429,
        reporterStatus: 2,
        unreadMessages: 1,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Vasya Devovich',
            text: 'Use the "n" key on your keyboard'
          }
        ]
      },
      {
        id: 5469,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 3,
        unreadMessages: 99,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "n" key on your keyboard'
          },
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question'
          },
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information ...'
          },
          {
            id: 1,
            sender: 'Vasya Devovich',
            text: 'Use the "n" key on your keyboard'
          },
          {
            id: 0,
            sender: 'Alexey Nenavalny',
            text: 'A good question, please give me a minute, I will clarify the information...'
          },
        ]
      },
      {
        id: 5470,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        unreadMessages: 0,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information ...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "n" key on your keyboard'
          }
        ]
      },
      {
        id: 5471,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        unreadMessages: 0,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, give me, please, for a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Vasya Devovich',
            text: 'Use the "n" key on your keyboard'
          },
          {
            id: 0,
            sender: 'Alexey Nenavalny',
            text: 'A good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, give me, please, for a minute, I will clarify the information...'
          }
        ]
      },
      {
        id: 5472,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        unreadMessages: 0,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "N" key on your keyboard'
          }
        ]
      },
    ],

    myReports: [
      {
        id: 5468,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 429,
        reporterStatus: 2,
        rating: 4,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "n" key on your keyboard'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the key "N"'
          }
        ]
      },
      {
        id: 5470,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        rating: 2,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "n" key on your keyboard'
          }
        ]
      },
      {
        id: 5471,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        rating: 5,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "n" key on your keyboard'
          }
        ]
      },
      {
        id: 5472,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        rating: 5,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "n" key on your keyboard'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use'
          }
          ,
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'On your keyboard... '
          },
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, give me information...'
          },
        ]
      },
    ],

    reportsLogs: [
      {
        id: 5467,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 1,
        rating: 5,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text:'Good question, give me, please, for a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Kekovich',
            text: 'Use the "N" key on your keyboard'
          }
        ]
      },
      {
        id: 5469,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 3,
        rating: 3,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "N" key on your keyboard'
          }
        ]
      },
      {
        id: 5470,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        rating: 2,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information ...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "n" key on your keyboard'
}
        ]
      },
      {
        id: 5471,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        rating: 5,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, give me, please, for a minute, I will clarify the information ...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text:'Use the "n" key on your keyboard'
          }
        ]
      },
      {
        id: 5472,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        rating: 5,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "n" key on your keyboard'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use'
          }
          ,
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'On your keyboard... '
          },
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, give me information...'
          },
        ]
      },
      {
        id: 5473,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        rating: 5,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "n" key on your keyboard'
          },
          {
            id: 1,
            sender: 'Kekovich Devovich',
            text: 'Use'
          }
          ,
          {
            id: 1,
            sender: 'Kekovic Devovich',
            text: 'On your keyboard... '
          },
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, give me information...'
          },
        ]
      },
      {
        id: 5474,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        rating: 5,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'A good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "n" key on your keyboard'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use'
          }
          ,
          {
            id: 1,
            sender: 'Milt Kekovich',
            text: 'On your keyboard... '
          },
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, give me information...'
          },
        ]
      },
      {
        id: 5475,
        time: 1600182247800,
        reporterName: 'Sharmeek Cherlynnes',
        reporterId: 428,
        reporterStatus: 0,
        rating: 5,
        messages: [
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, please give me a minute, I will clarify the information...'
          },
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'Use the "n" key on your keyboard'
          },
          {
            id: 1,
            sender: 'Milt Testovich',
            text: 'Use'
          }
          ,
          {
            id: 1,
            sender: 'Milt Devovich',
            text: 'On your keyboard... '
          },
          {
            id: 0,
            sender: 'Sharmeek Cherlynnes',
            text: 'Good question, give me information...'
          },
        ]
      },
    ],

    reportsLogAdmins: [],

    quickActions: [
      {
        key: 0,
        name: 'ReportMenu_25',
        type: 'trigger'
      },
      {
        key: 4,
        name: 'ReportMenu_30',
        type: 'position'
      },
      {
        key: 1,
        name: 'ReportMenu_26',
        type: 'trigger'
      },
      {
        key: 2,
        name: 'ReportMenu_27',
        type: 'kick'
      },
      {
        key: 3,
        name: 'ReportMenu_28',
        type: 'trigger'
      }
    ],

    currentPage: 'ReportsList',
    currentChat: null,
  },

  mutations: {
    setCurrentPage: function (state, page) {
      state.currentPage = page
      state.currentChat = null
    },
    setCurrentChat: function (state, chat) {
      state.currentChat = chat
      if (chat != null && state.currentPage == 'ReportsList' && chat.adminName == null) {
        window.mp.trigger("reportMenu:trytakereport", chat.id)
      }
      if (chat != null) {
        const index = state.reports.findIndex(item => item.id == chat.id)
        if (index > -1)
          state.reports[index].unreadMessages = 0;
      }
    },
    refreshCurrentChat: function (state) {
      state.currentChat = null
    },
    deleteAnswerTemplate: function (state, index) {
      state.answersTemplates.splice(index, 1)
    },
    addAnswerTemplate: function (state, text) {
      state.answersTemplates.push(text)
    },
    setMySettingsName: function (state, value) {
      state.mySettings.name = value;
    },
    setAnswersTemplates: function (state, value) {
      state.answersTemplates = value;
    },
    setReportsLogAdmins: function (state, value) {
      state.reportsLogAdmins = value;
    },
    addReportsLogAdmins: function (state, value) {
      if (value != null && state.reportsLogAdmins.findIndex(item => item == value) == -1)
        state.reportsLogAdmins.push(value)
    },

    setReports: function (state, value) {
      state.reports = value;
      state.reports.sort(function (a, b) {
        return b.reporterStatus - a.reporterStatus;
      })
    },
    addReport: function (state, value) {
      const index = state.reports.findIndex(item => item.id == value);
      if (index == -1)
        state.reports.push(value);
      else
        state.reports[index] = value;
      state.reports.sort(function (a, b) {
        return b.reporterStatus - a.reporterStatus;
      })
    },
    deleteReport: function (state, value) {
      const index = state.reports.findIndex(item => item.id == value);
      if (index != -1)
        state.reports.splice(index, 1);
      if (state.currentPage == 'ReportsList' && state.currentChat.id == value)
        state.currentChat = null
    },
    addReportMessage: function (state, value) {
      const index = state.reports.findIndex(item => item.id == value.reportId);
      if (index != -1) {
        state.reports[index].messages.push(value);
        if (state.chat == null || state.chat.id != state.reports[index].id)
          state.reports[index].unreadMessages ++
      }
    },

    setMyReports: function (state, value) {
      state.myReports = value;
    },
    addMyReport: function (state, value) {
      state.myReports.unshift(value);
    },

    setReportsLogs: function (state, value) {
      state.reportsLogs = value;
    },
    addReportLogs: function (state, value) {
      const index = state.reportsLogs.findIndex(item => item.id == value.id);
      if (index == -1)
        state.reportsLogs.unshift(value);
      else
        state.reportsLogs[index] = value;
    },
    deleteReportLogs: function (state, value) {
      const index = state.reportsLogs.findIndex(item => item.id == value);
      if (index != -1)
        state.reportsLogs.splice(index, 1);
      if (state.currentPage == 'ReportsLogs' && state.currentChat.id == value)
        state.currentChat = null
    },
    addReportLogsMessage: function (state, value) {
      const index = state.reportsLogs.findIndex(item => item.id == value.reportId);
      if (index != -1)
        state.reportsLogs[index].messages.push(value);
    },
    updateAdminNameForReportLog: function (state, value) {
      const index = state.reportsLogs.findIndex(item => item.id == value.id);
      if (index != -1)
        state.reportsLogs[index].adminName = value.adminName
    },

    updateRating: function (state, value) {
      const index = state.reportsLogs.findIndex(item => item.id == value.id);
      if (index != -1)
        state.reportsLogs[index].rating = value.rating
      const myIndex = state.myReports.findIndex(item => item.id == value.id);
      if (myIndex != -1)
        state.myReports[index].rating = value.rating
    },

    setReportsInQueue: function (state, value) {
      state.reportsInQueue = value;
    },
    setReportsAnswered: function (state, value) {
      state.reportsAnswered = value;
    },
    setMessageModel: function(state, value) {
      state.messageModel = value
    },
    startPenaltiesSearch: function (state) {
      state.penaltiesLoading = true
      state.penaltiesSearched = true
    },
    setPenalties: function (state, value) {
      state.penalties = value
      state.penaltiesLoading = false
      state.penaltiesSearched = true
    },
    resetPenalties: function (state) {
      state.penalties = []
      state.penaltiesLoading = false
      state.penaltiesSearched = false
    },
    startOrganizationsRequest: function (state) {
      state.organizationsLoading = true
      state.organizationsError = ''
    },
    setOrganizations: function (state, value) {
      state.organizations = value
      state.organizationsLoading = false
      state.organizationsError = ''
    },
    setOrganizationsError: function (state, value) {
      state.organizations = []
      state.organizationsLoading = false
      state.organizationsError = value || 'Failed to load organizations'
    },
    startBusinessesRequest: function (state) {
      state.businessesLoading = true
    },
    setBusinesses: function (state, value) {
      state.businesses = value
      state.businessesLoading = false
    },
    startAdministratorsRequest: function (state) {
      state.administratorsLoading = true
      state.administratorsError = ''
    },
    setAdministrators: function (state, value) {
      state.administrators = value
      state.administratorsLoading = false
      state.administratorsError = ''
    },
    setAdministratorsError: function (state, value) {
      state.administrators = []
      state.administratorsLoading = false
      state.administratorsError = value || 'Failed to load administrators'
    },
    startTeleportsRequest: function (state) {
      state.teleportsLoading = true
    },
    setTeleports: function (state, value) {
      state.teleports = value
      state.teleportsLoading = false
    },
    startFamiliesRequest: function (state) {
      state.familiesLoading = true
      state.familiesError = ''
    },
    setFamilies: function (state, value) {
      state.families = value
      state.familiesLoading = false
      state.familiesError = ''
    },
    setFamiliesError: function (state, value) {
      state.families = []
      state.familiesLoading = false
      state.familiesError = value || 'Failed to load families'
    },
    startCommandsRequest: function (state) {
      state.commandsLoading = true
      state.commandsError = ''
    },
    setCommands: function (state, value) {
      state.commands = value
      state.commandsLoading = false
      state.commandsError = ''
    },
    setCommandsError: function (state, value) {
      state.commands = []
      state.commandsLoading = false
      state.commandsError = value || 'Failed to load commands'
    },
    startEventEntriesRequest: function (state) {
      state.eventEntriesLoading = true
    },
    setEventEntries: function (state, value) {
      state.eventEntries = value
      state.eventEntriesLoading = false
    },
    addEventEntry: function (state, value) {
      state.eventEntries.unshift(value)
    },
    startMurdersRequest: function (state) {
      state.murdersLoading = true
    },
    setMurders: function (state, value) {
      state.murders = value
      state.murdersLoading = false
    },
    startArenaRequest: function (state) {
      state.arenaLoading = true
    },
    setArenaSessions: function (state, value) {
      state.arenaSessions = value
      state.arenaLoading = false
    },
    addStaffChatMessage: function (state, value) {
      const nextId = state.staffChatMessages.length > 0
        ? state.staffChatMessages[state.staffChatMessages.length - 1].id + 1
        : 1
      state.staffChatMessages.push({
        id: nextId,
        playerName: value.playerName,
        playerId: value.playerId,
        date: value.date,
        time: value.time,
        author: value.author,
        meta: value.meta,
        text: value.text
      })
    }
  }
}
